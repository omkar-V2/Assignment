using System.Security.Cryptography;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static Common.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class OrderController : ControllerBase
    {
        private readonly IDbOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IDbOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetAllOrder();
        }

        // GET api/<OrderController>/5
        [HttpGet("{orderno}")]
        public IEnumerable<Order> Get(string orderNo)
        {
            return _orderService.GetOrderByOrderNo(orderNo);
        }

        // GET /api/Order/Customer/2023
        [HttpGet("GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInPartsOfDayOfFirstMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("OrderController:Method:GetCustomerMadePurchasesInPartsOfDayOfFirstMonthOfYear called.");
            try
            {
                var rawResult = _orderService.GetCustomerMadePurchasesInPartsOfDayOfFirstMonthOfYear(month, year, partOfDay);

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


        // GET /api/Order/GetTotalNoOfOrderPlacedInLast3Month/2023
        [HttpGet("GetTotalNoOfOrderPlacedInLast3Month")]
        public ActionResult<IEnumerable<object>> GetTotalNoOfOrderPlacedInLast3Month()
        {
            _logger.LogInformation("OrderController:Method:GetTotalNoOfOrderPlacedInLast3Month called.");
            try
            {
                var result = _orderService.GetTotalNoOfOrderPlacedInLast3Month();

                return Ok(result);
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
                var rawResult = _orderService.GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month();

                return Ok($"Total No Of Unique Customers Placed Order In Last 3 Month: {string.Join(",", rawResult)}");

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

                var rawResult = _orderService.GetGroupOfCustomersPlacedOrderInLast3Month();

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
                var rawResult = _orderService.GetOrderCountPerMonthByYear(year);

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

    }
}
