using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using EmployeeManagement.Data;
using System.Runtime.ConstrainedExecution;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IDbCustomerService _dbCustomerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IDbCustomerService dbCustomerService, ILogger<CustomerController> logger)
        {
            _dbCustomerService = dbCustomerService;
            _logger = logger;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //Get: api/Customer/GetPurchaseAndOrderInfo/customerId
        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<object>> GetPurchaseAndOrderInfo(string customerId)
        {
            _logger.LogInformation("CustomerController:Method:GetPurchaseAndOrderInfo called.");
            try
            {
                var rawResult = _dbCustomerService.GetPurchaseAndOrderInfo(customerId);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetPurchaseAndOrderInfo Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetLast3MonthsCustomerOrderInfoOfPurchases/customerId
        [HttpGet("GetLast3MonthsCustomerOrderInfoOfPurchases/{customerId}")]
        public ActionResult<IEnumerable<object>> GetLast3MonthsCustomerOrderInfoOfPurchases(string customerId)
        {
            _logger.LogInformation("CustomerController:Method:GetLast3MonthsCustomerOrderInfoOfPurchases called.");
            try
            {
                var rawResult = _dbCustomerService.GetLast3MonthsCustomerOrderInfoOfPurchases(customerId);

                if (rawResult.Any())
                {
                    return Ok(new { rawResult });
                }

                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetLast3MonthsCustomerOrderInfoOfPurchases Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetLast3MonthsCustomerPurchaseInfo/customerId
        [HttpGet("GetLast3MonthsCustomerPurchaseInfo/{customerId}")]
        public ActionResult<IEnumerable<object>> GetLast3MonthsCustomerPurchaseInfo(string customerId)
        {
            _logger.LogInformation("CustomerController:Method:GetLast3MonthsCustomerPurchaseInfo called.");
            try
            {
                var rawResult = _dbCustomerService.GetLast3MonthsCustomerPurchaseInfo(customerId);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetLast3MonthsCustomerPurchaseInfo Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetCustomerLoyaltyTiers
        [HttpGet("GetCustomerLoyaltyTiers")]
        public ActionResult<IEnumerable<object>> GetCustomerLoyaltyTiers()
        {
            _logger.LogInformation("CustomerController:Method:GetCustomerLoyaltyTiers called.");
            try
            {
                var rawResult = _dbCustomerService.GetCustomerLoyaltyTiers();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }
                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetCustomerLoyaltyTiers Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetUniqueCustomerInteractedInLast3Month
        [HttpGet("GetUniqueCustomerInteractedInLast3Month")]
        public ActionResult<IEnumerable<object>> GetUniqueCustomerInteractedInLast3Month()
        {
            _logger.LogInformation("CustomerController:Method:GetUniqueCustomerInteractedInLast3Month called.");
            try
            {
                var rawResult = _dbCustomerService.GetUniqueCustomerInteractedInLast3Month();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetUniqueCustomerInteractedInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetMostActiveCustomerInLast3Month
        [HttpGet("GetMostActiveCustomerInLast3Month")]
        public ActionResult<object> GetMostActiveCustomerInLast3Month()
        {
            _logger.LogInformation("CustomerController:Method:GetMostActiveCustomerInLast3Month called.");
            try
            {
                var fromDatePurchase = DateTime.Now.AddMonths(-3);

                var rawResult = _dbCustomerService.GetMostActiveCustomerInLast3Month();

                if (rawResult is not null)
                {
                    return Ok(rawResult);
                }
                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetMostActiveCustomerInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
