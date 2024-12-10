using System.Security.Cryptography;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DbService _dbService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DbService dbService, ILogger<OrderController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _dbService.GetAllOrder();
        }

        // GET api/<OrderController>/5
        [HttpGet("{orderno}")]
        public IEnumerable<Order> Get(string orderno)
        {
            return _dbService.GetAllOrder().Where(ord => ord.OrderNo == orderno);
        }

        // GET /api/Order/Customer/2023
        [HttpGet("GetIQuerCustomerMadePurchasesInafternoonOfFirstMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetIQuerCustomerMadePurchasesInafternoonOfFirstMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("OrderController:Method:GetIQuerCustomerMadePurchasesInafternoonOfFirstMonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllOrderIQ()
                    .Where(ord => ord.OrderDateTime.Year == year && ord.OrderDateTime.Month == month && GetTimeOftheDay(ord.OrderDateTime.TimeOfDay) == partOfDay)
                    .Select(cus => cus.CustomerId)
                    .Distinct();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetIQuerCustomerMadePurchasesInafternoonOfFirstMonthOfYear Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Order/Customer/2023
        [HttpGet("GetIenumCustomerMadePurchasesInafternoonOfFirstMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetIenumCustomerMadePurchasesInafternoonOfFirstMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("OrderController:Method:GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllOrder(ord => (ord.OrderDateTime.Year == year
                                && ord.OrderDateTime.Month == month
                                && GetTimeOftheDay(ord.OrderDateTime.TimeOfDay) == partOfDay))
                                    .Select(cus => cus.CustomerId)
                                    .Distinct();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        // GET /api/Order/GetTotalNoOfOrderPlacedInLast3Month/2023
        [HttpGet("GetTotalNoOfOrderPlacedInLast3Month")]
        public ActionResult<IEnumerable<object>> GetTotalNoOfOrderPlacedInLast3Month()
        {
            _logger.LogInformation("OrderController:Method:GetTotalNoOfOrderPlacedInLast3Month called.");
            try
            {
                //var fromDate = _dbService.GetAllOrder().Max(ord => ord.OrderDateTime).AddMonths(-3);

                var fromDate = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllOrder(get => get.OrderDateTime > fromDate);
                var result = rawResult.Count();

                return Ok(new
                {
                    message = $"Total No Of Order Placed In Last 3 Month:{result}",
                    rawResult
                });
                // return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetTotalNoOfOrderPlacedInLast3Month Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Order/GetTotalUniqueCustomersPlacedOrderInLast3Month/2023
        [HttpGet("GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month")]
        public ActionResult<IEnumerable<object>> GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month()
        {
            _logger.LogInformation("OrderController:Method:GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month called.");
            try
            {
                // var fromDate = _dbService.GetAllOrder().Max(ord => ord.OrderDateTime).AddMonths(-3);

                var fromDate = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllOrder(get => get.OrderDateTime > fromDate)
                                .Select(ord => ord.CustomerId)
                                .Distinct()
                                .Count();

                return Ok($"Total No Of Unique Customers Placed Order In Last 3 Month: {rawResult}");

                // return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Order/GetTotalUniqueCustomersPlacedOrderInLast3Month/2023
        [HttpGet("GetGroupOfCustomersPlacedOrderInLast3Month")]
        public ActionResult<IEnumerable<object>> GetGroupOfCustomersPlacedOrderInLast3Month()
        {
            _logger.LogInformation("OrderController:Method:GetGroupOfCustomersPlacedOrderInLast3Month called.");
            try
            {
                var fromDate = new DateTime(2023, 12, 31).AddMonths(-3);

                //var fromDate = _dbService.GetAllOrder().Max(ord => ord.OrderDateTime).AddMonths(-3);

                var rawResult = _dbService.GetAllOrderIQ()
                                .Where(get => get.OrderDateTime > fromDate)
                                .GroupBy(ord => new
                                {
                                    ord.OrderDateTime.Year,
                                    ord.OrderDateTime.Month
                                })
                                .Select(ord => new
                                {
                                    orderdateyear = ord.Key.Year,
                                    orderdatemonth = ord.Key.Month,
                                    customers = string.Join(",", ord.Select(ord => ord.CustomerId))
                                })
                                .OrderBy(ord => ord.orderdatemonth);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetGroupOfCustomersPlacedOrderInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Order/GetTotalNoOfOrderPlacedInLast3Month/2023
        [HttpGet("GetOrderCountPerMonthByYear")]
        public ActionResult<IEnumerable<object>> GetOrderCountPerMonthByYear(int year)
        {
            _logger.LogInformation("OrderController:Method:GetOrderCountPerMonthByYear called.");
            try
            {
                var rawResult = _dbService.GetAllOrderIQ()
                                .Where(ord => ord.OrderDateTime.Year == year)
                                .SelectMany(product => product.Products, (ord, product) => new
                                {
                                    orderyear = ord.OrderDateTime.Year,
                                    ordermonth = ord.OrderDateTime.Month,
                                    orderno = ord.OrderNo,
                                    product = product.Title
                                })
                                .GroupBy(group1 => new
                                {
                                    group1.orderyear,
                                    group1.ordermonth
                                })
                                .Select(result => new
                                {
                                    year = result.Key.orderyear,
                                    month = result.Key.ordermonth,
                                    //product = result.GroupBy(prd => prd.product)
                                    //         .Select(ord => new { Title = ord.Key, ProdCount = ord.Count() }),
                                    totalproductcount = result.Count()
                                })
                               .OrderBy(ord => ord.month);


                if (rawResult.Any())
                {
                    return Ok(new { rawResult });
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderController:Method:GetOrderCountPerMonthByYear Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        private static PartOfDay GetTimeOftheDay(TimeSpan timeSpan)
        {
            if (timeSpan.Hours >= 5 && timeSpan.Hours < 12)
                return PartOfDay.Morning;
            else if (timeSpan.Hours >= 12 && timeSpan.Hours < 17)
                return PartOfDay.Afternoon;
            else if (timeSpan.Hours >= 17 && timeSpan.Hours < 21)
                return PartOfDay.Evening;
            else
                return PartOfDay.Night;
        }

        public enum PartOfDay
        {
            Morning,
            Afternoon,
            Evening,
            Night
        }


    }
}
