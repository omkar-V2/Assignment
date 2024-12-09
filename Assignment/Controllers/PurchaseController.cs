using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static CCMPreparation.Controllers.OrderController;

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
                var fromDatePurchase = _dbService.GetAllPurchase().Max(sup => sup.PurchaseDate).AddMonths(-3);

                var rawResult1 = Helpers.GetMedianAmt(_dbService
                                                     .GetAllPurchase()
                                                     .Where(pur => pur.PurchaseDate > fromDatePurchase)
                                                     .Select(pur1 => pur1.Amount)
                                                     .OrderBy(ord => ord));

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
                                    ListedAmts = string.Join(",", cus.Select(amt => amt.Amount).Order())
                                })
                                .TakeLast(3);

                if (rawResult.Any())
                {
                    return Ok(new { rawResult, message = $"Customer Median Purchase Amt In Last 3 Month Of Year:{rawResult1}" });
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMedianPurchaseAmtInLast3MonthOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMadePurchasesInLast6MonthOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInLast6MonthOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMadePurchasesInLast6MonthOfYear called.");
            try
            {
                var fromDatePurchase = new DateTime(year, 12, 31).AddMonths(-6);


                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate > fromDatePurchase)
                                .Select(cus => cus.CustomerId)
                                .Distinct();
                if (rawResult.Any())
                {
                    return Ok($"Customer= {string.Join(",", rawResult)}");
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMadePurchasesInLast6MonthOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMadePurchasesInYear/{year}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMadePurchasesInYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate.Year == year)
                                .OrderBy(month => month.PurchaseDate.Month)
                                .Select(cus => cus.CustomerId)
                                .Distinct();

                if (rawResult.Any())
                {
                    return Ok($"Customer= {string.Join(",", rawResult)}");
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMadePurchasesInYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnEachDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnEachDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate.Year == year)
                                .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                .Select(record => new
                                {
                                    Day = record.Key,
                                    Count = record.Count()
                                });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnEachDaysInLast3Months")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnEachDaysInLast3Months()
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysInLast3Months called.");
            try
            {
                var fromDate = _dbService.GetAllPurchase().Max(pur => pur.PurchaseDate).AddMonths(-3);

                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate > fromDate)
                                .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                .Select(record => new
                                {
                                    Day = record.Key,
                                    Count = record.Count()
                                });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysInLast3Months Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetAveragePurchasesMadeOnEachDaysOfYear/year/2023
        [HttpGet("GetAveragePurchasesMadeOnEachDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetAveragePurchasesMadeOnEachDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetAveragePurchasesMadeOnEachDaysOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate.Year == year)
                                .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                .Select(record => new
                                {
                                    Day = record.Key,
                                    Average = record.Average(avg => avg.Amount)
                                });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetAveragePurchasesMadeOnEachDaysOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnWeekDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnWeekDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnWeekDaysOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate.Year == year &&
                                 !(pur.PurchaseDate.DayOfWeek == DayOfWeek.Saturday || pur.PurchaseDate.DayOfWeek == DayOfWeek.Sunday))
                                .Select(record => new
                                {
                                    amount = record.Amount,
                                    orderTime = record.PurchaseDate,
                                    customerId = record.CustomerId
                                });

                if (rawResult.Any())
                {
                    return Ok($"Total Purchase on WeekDays:{rawResult.Count()}");
                    // return Ok(rawResult);

                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnWeekDaysOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }


        // GET /api/Purchae/GetTotalPurchasesMadeOnWeekendfYear/year/2023
        [HttpGet("GetTotalPurchasesMadeOnWeekendfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnWeekendfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnWeekendfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.PurchaseDate.Year == year &&
                                 (pur.PurchaseDate.DayOfWeek == DayOfWeek.Saturday || pur.PurchaseDate.DayOfWeek == DayOfWeek.Sunday))
                                .Select(record => new
                                {
                                    amount = record.Amount,
                                    orderTime = record.PurchaseDate,
                                    customerId = record.CustomerId
                                });

                if (rawResult.Any())
                {
                    return Ok($"Total Purchase on Weekend:{rawResult.Count()}");
                    //return Ok(rawResult);

                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnWeekendfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetHighestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetHighestPurchasesMadeOnDayOfWeekOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetHighestPurchasesMadeInDayOfWeekOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetHighestPurchasesMadeInDayOfWeekOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(ord => ord.PurchaseDate.Year == year)
                                .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                .Select(result => new
                                {
                                    Day = result.Key,
                                    PurchaseCount = result.Count()
                                });

                var finalResult = rawResult.Where(result => result.PurchaseCount == rawResult.Max(purcount => purcount.PurchaseCount));

                if (finalResult.Any())
                {
                    return Ok(finalResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetHighestPurchasesMadeInDayOfWeekOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetLowestPurchasesMadeOnDayOfWeekOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetLowestPurchasesMadeInDayOfWeekOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetLowestPurchasesMadeInDayOfWeekOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Where(ord => ord.PurchaseDate.Year == year)
                                .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                .Select(result => new
                                {
                                    Day = result.Key,
                                    PurchaseCount = result.Count()
                                });

                var finalResult = rawResult.Where(result => result.PurchaseCount == rawResult.Min(purcount => purcount.PurchaseCount));

                if (finalResult.Any())
                {
                    return Ok(finalResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetLowestPurchasesMadeInDayOfWeekOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET /api/Purchase/GetTotalPurchasesMadeInMonthOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeInMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeInMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeInMonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                                .Count(pur => pur.PurchaseDate.Year == year
                                 && pur.PurchaseDate.Month == month
                                 && Helpers.GetTimeOftheDay(pur.PurchaseDate.TimeOfDay) == partOfDay);

                if (rawResult > 0)
                {
                    return Ok($"Total Purchases= {rawResult}");
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeInMonthOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
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
