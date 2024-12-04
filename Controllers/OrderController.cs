using System.Security.Cryptography;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static CCMPreparation.Controllers.OrderController;

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
        [HttpGet("GetCustomerMadePurchaseInMonthOfYear/month/{month}/year/{year}/TimofDay/{partOfDay}")]
        public ActionResult<IEnumerable<object>> GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            _logger.LogInformation("OrderController:Method:GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllOrder()
                                .Where(ord => ord.OrderDateTime.Year == year && ord.OrderDateTime.Month == month && GetTimeOftheDay(ord.OrderDateTime.TimeOfDay) == partOfDay)
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
                _logger.LogError("OrderController:Method:GetCustomerMadePurchasesInafternoonOfFirstMonthOfYear Error: {ex}", ex);

                var json = JsonConvert.SerializeObject(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
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


        //// POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
