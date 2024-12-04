using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly DbService _dbService;
        private readonly ILogger<OrderController> _logger;

        public PurchaseController(DbService dbService, ILogger<OrderController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        // GET: api/<Purchase>
        [HttpGet]
        public IEnumerable<Purchase> Get()
        {
            return _dbService.GetAllPurchase();
        }

        // GET api/<Purchase>/5
        [HttpGet("{customerId}")]
        public IEnumerable<Purchase> GetPurchaseByCustomerID(string customerId)
        {
            return _dbService.GetAllPurchase().Where(pur => pur.CustomerId == customerId);
        }

        // GET api/<Purchase>/year/2023
        [HttpGet("year/{year}")]
        public IEnumerable<object> GetCustomerExpenditureByYear(int year)
        {

            var nestedGroupsQuery = _dbService.GetAllPurchase()
             .GroupBy(pur => pur.PurchaseDate.Year == year)
             .Select(newGroup1 => new
             {
                 newGroup1.Key,
                 NestedGroup = newGroup1.GroupBy(group => group.CustomerId)
             });

            foreach (var outerGroup in nestedGroupsQuery)
            {
                Console.WriteLine($"outerGroup key type= {outerGroup.Key.GetType()} value= {outerGroup.Key}");
                foreach (var innerGroup in outerGroup.NestedGroup)
                {
                    Console.WriteLine($"innerGroup key type= {innerGroup.Key.GetType()} value= {innerGroup.Key}");
                    Console.WriteLine($"innerGroup Amount   value= {innerGroup.Sum(pur => pur.Amount)}");

                    foreach (var innerGroupElement in innerGroup)
                    {
                        Console.WriteLine($"innerGroup Customer= {innerGroupElement.CustomerId} Amount= {innerGroupElement.Amount}");
                    }
                }
            }

            return _dbService.GetAllPurchase()
                .Where(pur => pur.PurchaseDate.Year == year)
                .GroupBy(group => group.CustomerId)
                .Select(customergroup => new
                {
                    customer = customergroup.Key,
                    TotalAmount = customergroup.Sum(pur => pur.Amount)
                });
        }

        // GET api/<Purchase>/month/2023
        [HttpGet("month/{year}")]
        public IEnumerable<object> GetTopCustomerWithHighExpenditureByMonthOfYear(int year)
        {
            var rawResult = _dbService.GetAllPurchase()
             .Where(pur => pur.PurchaseDate.Year == year)
             .GroupBy(group => new { group.PurchaseDate.Month, group.CustomerId })
             .Select(customergroup => new
             {
                 Month = customergroup.Key.Month,
                 Customer = customergroup.Key.CustomerId,
                 TotalAmnt = customergroup.Sum(group => group.Amount)
             });

            var result = rawResult.GroupBy(month => month.Month)
                                  .Select(g => g.OrderByDescending(t => t.TotalAmnt).First())
                                  .OrderBy(ord => ord.Month);

            return result;
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerPurchasHistory/customer/{customerId}")]
        public ActionResult<IEnumerable<object>> GetCustomerPurchasHistory(string customerId)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerPurchaseHistory called");

            try
            {
                if (customerId == null)
                {
                    return BadRequest();
                }


                var rawResult = _dbService.GetAllPurchase()
                               .Where(cust => cust.CustomerId == customerId)
                               .OrderBy(cus => cus.PurchaseDate);
                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = $"No data found for customer:{customerId}" });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerPurchaseHistory Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMinMaxAveragePurchaseAmt")]
        public ActionResult<IEnumerable<object>> GetCustomerMinMaxAveragePurchaseAmt()
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMinMaxAveragePurchaseAmt called");

            try
            {
                var rawResult = _dbService.GetAllPurchase()
                               .GroupBy(group => group.CustomerId)
                               .Select(cus => new
                               {
                                   Customer = cus.Key,
                                   Min = cus.Min(cus => cus.Amount),
                                   Max = cus.Max(cus => cus.Amount),
                                   Average = cus.Average(cus => cus.Amount)
                               });
                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMinMaxAveragePurchaseAmt Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerAveragePurchaseAmtByMonthOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetCustomerAveragePurchaseAmtByMonthOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerAveragePurchaseAmtByMonthOfYear called.");
            try
            {
                if (year <= 0)
                {
                    return BadRequest();
                }
                var rawResult = _dbService.GetAllPurchase()
                               .GroupBy(group => new { Year = group.PurchaseDate.Year, Month = group.PurchaseDate.Month })
                               .Select(cus => new
                               {
                                   cus.Key.Year,
                                   cus.Key.Month,
                                   Average = cus.Average(cus => cus.Amount)
                               });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerAveragePurchaseAmtByMonthOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMedianPurchaseAmtInLast3MonthOfYear")]
        public ActionResult<IEnumerable<object>> GetCustomerMedianPurchaseAmtInLast3MonthOfYear()
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMedianPurchaseAmtInLast3MonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .GroupBy(group => new
                                {
                                    Year = group.PurchaseDate.Year,
                                    Month = group.PurchaseDate.Month
                                })
                                .Select(cus => new
                                {
                                    cus.Key.Year,
                                    cus.Key.Month,
                                    ListedAmts = string.Join(",", cus.Select(amt => amt.Amount).Order()),
                                    MedianAmt = GetMedianAmt(cus.OrderBy(ord => ord.Amount))
                                })
                                .Take(3);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMedianPurchaseAmtInLast3MonthOfYear Error: {ex}", ex);

                var json = JsonConvert.SerializeObject(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        private static int GetMedianAmt(IOrderedEnumerable<Purchase> cus)
        {
            if (!cus.Any())
                return 0;

            var count = cus.Count();

            if (count / 2 != 0 && count % 2 == 0) //even
            {
                return ((cus.ElementAt((count / 2) - 1)).Amount +
                         (cus.ElementAt(count / 2)).Amount) / 2;
            }
            else if (count / 2 != 0 && count % 2 != 0) //odd
            {
                return (cus.OrderBy(ord => ord.Amount).ElementAt((count / 2))).Amount;
            }
            else // for one element
            {
                return cus.ElementAt(0).Amount;
            }
        }

        //// GET /api/<Purchase>/year/2023
        //[HttpGet("/year/{year}")]
        //public ActionResult<IEnumerable<object>> GetMostSoldProductByYear(int year)
        //{
        //    _logger.LogInformation("PurchaseController:Method:GetMostSoldProductByYear Called");

        //    try
        //    {
        //        var rawResult = _dbService.GetAllPurchase()
        //                      .Where(yr => yr.PurchaseDate.Year == year)
        //                      .GroupBy(group => new { Year = group.PurchaseDate.Year, Product=group.});



        //        return Ok();


        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("PurchaseController:Method:GetMostSoldProductByYear: {ex}", ex);
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }

        //}


        //// POST api/<Purchase>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<Purchase>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Purchase>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
