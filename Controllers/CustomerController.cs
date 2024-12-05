using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
                var rawResult = _dbService.GetAllPurchase().Join(_dbService.GetAllOrder(), purchase => purchase.CustomerId, order => order.CustomerId, (purchase, order) => new
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

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        //Get: api/Customer/GetLast3MonthsCustomerOrderInfoOfPurchases/customerId
        [HttpGet("GetLast3MonthsCustomerOrderInfoOfPurchases/{customerId}")]
        public ActionResult<IEnumerable<object>> GetLast3MonthsCustomerOrderInfoOfPurchases(string customerId)
        {
            _logger.LogInformation("CustomerController:Method:GetLast3MonthsCustomerOrderInfoOfPurchases called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase().Where(pur => pur.CustomerId == customerId)
                    .Join(_dbService.GetAllOrder().Where(ord => ord.CustomerId == customerId), purchase => purchase.CustomerId, order => order.CustomerId, (purchase, order) => new
                    {
                        purchase.PurchaseDate,
                        order.OrderDateTime,
                        order.OrderNo,
                        Product = order.Products
                    })
                .OrderBy(result => result.PurchaseDate.Month)
                .TakeLast(3);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetLast3MonthsCustomerOrderInfoOfPurchases Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        //Get: api/Customer/GetLast3MonthsCustomerOrderInfoOfPurchases/customerId
        [HttpGet("GetLast3MonthsCustomerPurchaseInfo/{customerId}")]
        public ActionResult<IEnumerable<object>> GetLast3MonthsCustomerPurchaseInfo(string customerId)
        {
            _logger.LogInformation("CustomerController:Method:GetLast3MonthsCustomerPurchaseInfo called.");
            try
            {
                var rawResult = _dbService.GetAllPurchase()
                    .Where(pur => pur.CustomerId == customerId)
                    .Join(_dbService.GetAllOrder().Where(ord => ord.CustomerId == customerId), purchase => purchase.CustomerId, order => order.CustomerId, (purchase, order) => new
                    {
                        purchase.CustomerId,
                        purchase.Amount,
                        purchase.PurchaseDate
                    })
                .OrderBy(result => result.PurchaseDate.Month)
                .TakeLast(3);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController:Method:GetLast3MonthsCustomerPurchaseInfo Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }


    }
}
