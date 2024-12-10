using Common;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static CCMPreparation.Controllers.OrderController;
using static Common.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IDbPurchaseService _purchaseService;
        private readonly ILogger<OrderController> _logger;

        public PurchaseController(IDbPurchaseService purchaseService, ILogger<OrderController> logger)
        {
            _purchaseService = purchaseService;
            _logger = logger;
        }

        // GET: api/<Purchase>
        [HttpGet]
        public IEnumerable<Purchase> Get()
        {
            return _purchaseService.GetAllPurchase();
        }

        // GET api/<Purchase>/5
        [HttpGet("{customerId}")]
        public IEnumerable<Purchase> GetPurchaseByCustomerID(string customerId)
        {
            return _purchaseService.GetPurchaseByCustomerID(customerId);
        }

        // GET api/<Purchase>/year/2023
        [HttpGet("year/{year}")]
        public IEnumerable<object> GetCustomerExpenditureByYear(int year)
        {

            return _purchaseService.GetCustomerExpenditureByYear(year);
        }

        // GET api/<Purchase>/month/2023
        [HttpGet("month/{year}")]
        public IEnumerable<object> GetTopCustomerWithHighExpenditureByMonthOfYear(int year)
        {
            var result = _purchaseService.GetTopCustomerWithHighExpenditureByMonthOfYear(year);

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

                var rawResult = _purchaseService.GetCustomerPurchasHistory(customerId);

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
                var rawResult = _purchaseService.GetCustomerMinMaxAveragePurchaseAmt();

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
                var rawResult = _purchaseService.GetCustomerAveragePurchaseAmtByMonthOfYear(year);

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
        public ActionResult<IEnumerable<object>> GetCustomerMedianPurchaseAmtInLast3Month()
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMedianPurchaseAmtInLast3Month called.");
            try
            {
                var rawResult = _purchaseService.GetCustomerMedianPurchaseAmtInLast3Month();

                return Ok($"Customer Median Purchase Amt In Last 3 Month Of Year:{rawResult}");

                // return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMedianPurchaseAmtInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMadePurchasesInLast6MonthOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInLast6MonthOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMadePurchasesInLast6MonthOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetCustomerMadePurchasesInLast6MonthOfYear(year);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMadePurchasesInLast6MonthOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/Customer/C001
        [HttpGet("GetCustomerMadePurchasesInYear/{year}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetCustomerMadePurchasesInYear called.");
            try
            {
                var rawResult = _purchaseService.GetCustomerMadePurchasesInYear(year);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetCustomerMadePurchasesInYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnEachDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnEachDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetTotalPurchasesMadeOnEachDaysOfYear(year);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnEachDaysInLast3Months")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnEachDaysInLast3Months()
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysInLast3Months called.");
            try
            {

                var rawResult = _purchaseService.GetTotalPurchasesMadeOnEachDaysInLast3Months();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnEachDaysInLast3Months Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetAveragePurchasesMadeOnEachDaysOfYear/year/2023
        [HttpGet("GetAveragePurchasesMadeOnEachDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetAveragePurchasesMadeOnEachDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetAveragePurchasesMadeOnEachDaysOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetAveragePurchasesMadeOnEachDaysOfYear(year);

                return Ok(new { message = $"Average purchase on each day of year:{rawResult}" });

            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetAveragePurchasesMadeOnEachDaysOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeOnWeekDaysOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnWeekDaysOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnWeekDaysOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetTotalPurchasesMadeOnWeekDaysOfYear(year);

                return Ok($"Total Purchase on WeekDays:{rawResult}");

            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnWeekDaysOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        // GET /api/Purchae/GetTotalPurchasesMadeOnWeekendfYear/year/2023
        [HttpGet("GetTotalPurchasesMadeOnWeekendfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeOnWeekendfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeOnWeekendfYear called.");
            try
            {
                var rawResult = _purchaseService.GetTotalPurchasesMadeOnWeekendfYear(year);

                return Ok($"Total Purchase on Weekend:{rawResult}");

            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeOnWeekendfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetHighestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetHighestPurchasesMadeOnDayOfWeekOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetHighestPurchasesMadeInDayOfWeekOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetHighestPurchasesMadeInDayOfWeekOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetHighestPurchasesMadeInDayOfWeekOfYear(year);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetHighestPurchasesMadeInDayOfWeekOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetLowestPurchasesMadeInDayOfWeekOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetLowestPurchasesMadeOnDayOfWeekOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetLowestPurchasesMadeInDayOfWeekOfYear(int year)
        {
            _logger.LogInformation("PurachesController:Method:GetLowestPurchasesMadeInDayOfWeekOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetLowestPurchasesMadeInDayOfWeekOfYear(year);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetLowestPurchasesMadeInDayOfWeekOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/Purchase/GetTotalPurchasesMadeInMonthOfYear/month/01/year/2023/TimofDay/Afternoon
        [HttpGet("GetTotalPurchasesMadeInMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetTotalPurchasesMadeInMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("PurachesController:Method:GetTotalPurchasesMadeInMonthOfYear called.");
            try
            {
                var rawResult = _purchaseService.GetTotalPurchasesMadeInMonthOfYear(month, year, partOfDay);

                return Ok($"Total Purchases= {rawResult}");

            }
            catch (Exception ex)
            {
                _logger.LogError("PurachesController:Method:GetTotalPurchasesMadeInMonthOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
