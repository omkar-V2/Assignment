using System.Text.Json;
using CCMPreparation.Controllers;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketController : ControllerBase
    {
        private readonly DbService _dbService;
        private readonly ILogger<SupportTicketController> _logger;

        public SupportTicketController(DbService dbService, ILogger<SupportTicketController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }


        // GET: api/<SupportTicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetDuplicateSupportActivity")]
        public ActionResult<object> GetDuplicateSupportActivity()
        {
            _logger.LogInformation("SupportTicketController:Method:GetDuplicateSupportActivity called.");
            try
            {
                var rawResult = _dbService.GetAllSupportTicket()
                    .GroupBy(supp => supp.Category)
                    .Select(supp => new
                    {
                        ticketname = supp.Key,
                        duplicatecount = supp.Count()
                    });

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetDuplicateSupportActivity Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        //// GET api/<SupportTicketController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<SupportTicketController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SupportTicketController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SupportTicketController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
