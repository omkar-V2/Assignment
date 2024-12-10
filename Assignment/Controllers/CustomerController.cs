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
        private readonly DbService _dbService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(DbService dbService, ILogger<CustomerController> logger)
        {
            _dbService = dbService;
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
                var rawResult = _dbService.GetAllPurchase()
                    .Join(_dbService.GetAllOrder(), purchase => purchase.CustomerId,
                    order => order.CustomerId, (purchase, order) => new
                    {
                        purchase.CustomerId,
                        purchase.Amount,
                        purchase.PurchaseDate,
                        order.OrderDateTime,
                        order.OrderNo,
                        Product = order.Products
                    });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
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
                //var fromDatePurchase = _dbService.GetAllPurchase().Max(sup => sup.PurchaseDate).AddMonths(-3);

                var fromDatePurchase = DateTime.Now.AddMonths(-3);


                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.CustomerId == customerId && pur.PurchaseDate > fromDatePurchase)
                                .Join(_dbService.GetAllOrder().Where(ord => ord.CustomerId == customerId),
                                  purchase => purchase.CustomerId, order => order.CustomerId,
                                  (purchase, order) => new
                                  {
                                      order.OrderNo,
                                      order.OrderDateTime
                                  });


                if (rawResult.Any())
                {
                    return Ok(new { rawResult });
                }

                return NotFound(new { message = "No data found for customer." });
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
                //var fromDatePurchase = _dbService.GetAllPurchase().Max(sup => sup.PurchaseDate).AddMonths(-3);

                var fromDatePurchase = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllPurchase()
                                .Where(pur => pur.CustomerId == customerId && pur.PurchaseDate > fromDatePurchase)
                                .Select(purchase => new
                                {
                                    purchase.Amount,
                                    purchase.PurchaseDate
                                })
                                .OrderBy(result => result.PurchaseDate.Month);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
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
                var rawResult = _dbService.GetAllCustomerActivity()
                                .GroupBy(group1 => new
                                {
                                    group1.ActivityDate.Year,
                                    group1.CustomerId
                                })
                                .Select(result => new
                                {
                                    year = result.Key.Year,
                                    customer = result.Key.CustomerId,
                                    loyaltytier = GetLoyaltyTier(result)
                                });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
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
                //var fromDatePurchase = _dbService.GetAllCustomerActivity().Max(sup => sup.ActivityDate).AddMonths(-3); 

                var fromDatePurchase = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllCustomerActivity()
                                .Where(pur => pur.ActivityDate > fromDatePurchase)
                                .Select(purchase => new
                                {
                                    purchase.CustomerId
                                })
                                .Distinct();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetUniqueCustomerInteractedInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get: api/Customer/GetMostActiveCustomerInLast3Month
        [HttpGet("GetMostActiveCustomerInLast3Month")]
        public ActionResult<IEnumerable<object>> GetMostActiveCustomerInLast3Month()
        {
            _logger.LogInformation("CustomerController:Method:GetMostActiveCustomerInLast3Month called.");
            try
            {
                //var fromDatePurchase = _dbService.GetAllCustomerActivity().Max(sup => sup.ActivityDate).AddMonths(-3);

                var fromDatePurchase = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllCustomerActivity()
                                .Where(pur => pur.ActivityDate > fromDatePurchase)
                                .GroupBy(purchase => new
                                {
                                    purchase.CustomerId,
                                })
                                .Select(cus => new { cus.Key.CustomerId, activecount = cus.Count() })
                                .MaxBy(active => active.activecount)
                                ;


                if (rawResult is not null)
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetMostActiveCustomerInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        public static string GetLoyaltyTier(IGrouping<object, CustomerActivity> loyalty)
        {
            //            -Platinum(12 or more purchases per year)
            //#           - Gold (6 to 11 purchases per year)
            //#           - Silver (1 to 5 purchases per year).

            int itemCount = loyalty.Count();
            if (itemCount < 12)
            {
                if (itemCount < 6)
                {
                    return "Silver";
                }
                else if (itemCount <= 0)
                {
                    return "None";
                }
                else
                {
                    return "Gold";
                }
            }
            else
            {
                return "Platinum";
            }



        }
    }
}
