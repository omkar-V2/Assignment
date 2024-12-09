using System.Text.Json;
using CCMPreparation.Controllers;
using EmployeeManagement.Data;
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
                    .GroupBy(supp => new { supp.Category, supp.SupportDateTime.Month })
                    .Select(supp => new
                    {
                        ticketname = supp.Key.Category,
                        month = supp.Key.Month,
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

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsAverageInLast3Month")]
        public ActionResult<object> GetSupportTicketsAverageInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsAverageInLast3Month called.");
            try
            {
                // var fromDate = _dbService.GetAllSupportTicket().Max(sup => sup.SupportDateTime).AddMonths(-3);

                var fromDate = DateTime.Now.AddMonths(-3);

                var rawResult1 = _dbService.GetAllSupportTicket()
                    .Where(supp => supp.SupportDateTime > fromDate)
                    .GroupBy(group => group.SupportDateTime.Month)
                    .Select(sup => new
                    {
                        month = sup.Key,
                        ticket = sup.Count()
                    })
                    .Average(tic => tic.ticket);

                var rawResult = _dbService.GetAllSupportTicket()
                     .Where(supp => supp.SupportDateTime > fromDate)
                     .GroupBy(group => group.SupportDateTime.Month)
                     .Select(sup => new
                     {
                         month = sup.Key,
                         ticket = sup.Count()
                     });

                return Ok(new
                {
                    message = $"Support Tickets Average In Last 3 Month:{rawResult1}",
                    rawResult
                });

                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsAverageInLast3Month Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsTotalNoPerCategoryInLast3Month")]
        public ActionResult<object> GetSupportTicketsTotalNoPerCategoryInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsTotalNoPerCategoryInLast3Month called.");
            try
            {
                //  var fromDate = _dbService.GetAllSupportTicket().Max(sup => sup.SupportDateTime).AddMonths(-3);

                var fromDate = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllSupportTicket()
                    .Where(supp => supp.SupportDateTime > fromDate)
                    .GroupBy(group => group.Category)
                     .Select(sup1 => new
                     {
                         category = sup1.Key,
                         ticketCount = sup1.Count()
                     });

                if (rawResult.Any())
                    return Ok(new { rawResult });

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsTotalNoPerCategoryInLast3Month Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsTotalNoPerMonthInLast3Month")]
        public ActionResult<object> GetSupportTicketsTotalInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsTotalNoPerMonthInLast3Month called.");
            try
            {
                // var fromDate = _dbService.GetAllSupportTicket().Max(sup => sup.SupportDateTime).AddMonths(-3);

                var fromDate = DateTime.Now.AddMonths(-3);

                var rawResult = _dbService.GetAllSupportTicket()
                                .Where(supp => supp.SupportDateTime > fromDate)
                                .GroupBy(group => group.SupportDateTime.Month)
                                .Select(sup => new
                                {
                                    month = sup.Key,
                                    ticket = sup.Count()
                                });
                if (rawResult.Any())
                    return Ok(new
                    {
                        rawResult
                    });

                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsTotalNoPerMonthInLast3Month Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsAveragePerMonth")]
        public ActionResult<object> GetSupportTicketsAveragePerMonth()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsAveragePerMonth called.");
            try
            {
                var rawResult = _dbService.GetAllSupportTicket()
                    .GroupBy(group => group.SupportDateTime.Month)
                    .Select(sup => new
                    {
                        month = sup.Key,
                        ticket = sup.Count()
                    });

                var result = rawResult.Average(tic => tic.ticket);

                return Ok(new
                {
                    rawResult = rawResult,
                    message = $"Support Tickets Average Per Month:{result}"
                });

                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsAveragePerMonth Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        //GEt api/<SupportTicketController>/GetMajorSupportTicketCountAndStatus
        [HttpGet("GetMajorSupportTicketCountAndStatus")]
        public ActionResult<IEnumerable<object>> GetMajorSupportTicketCountAndStatus()
        {
            _logger.LogInformation("SupportTicketController:Method:GetMajorSupportTicketCountAndStatus called.");
            try
            {
                var rawResult = _dbService.GetAllSupportTicket()
                      .GroupBy(supp => new { supp.Category, supp.SupportDateTime.Month })
                      .Select(supp => new
                      {
                          ticketname = supp.Key.Category,
                          ticketmonth = supp.Key.Month,
                          ticketcount = supp.Count(),
                          ticketstatus = GetStatus(supp)
                      });


                return Ok(new
                {
                    rawResult = rawResult,
                    // message = $"Support Tickets Average Per Month:{result}"
                });

                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetMajorSupportTicketCountAndStatus Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }

        }

        //GET api/<SupportTicketController>/GetHighestNoSupportTicketsMonthOfYear
        [HttpGet("GetHighestNoSupportTicketsMonthOfYear/{year}")]
        public ActionResult<IEnumerable<object>> GetHighestNoSupportTicketsMonthOfYear(int year)
        {
            _logger.LogInformation("SupportTicketController:Method:GetHighestNoSupportTicketsMonthOfYear called.");
            try
            {
                var rawResult = _dbService.GetAllSupportTicket()
                      .Where(tick => tick.SupportDateTime.Year == year)
                      .GroupBy(supp => supp.SupportDateTime.Month)
                      .Select(supp => new
                      {
                          ticketmonth = supp.Key,
                          ticketcount = supp.Count()
                      })
                      .OrderByDescending(high => high.ticketcount);

                if (rawResult.Any())
                    return Ok(new { MonthWithHighestNoOfSupportTickets = rawResult.MaxBy(tic => tic.ticketcount)?.ticketmonth, rawResult });

                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetHighestNoSupportTicketsMonthOfYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }
        }

        private static string GetStatus(IGrouping<object, SupportTicket> supp)
        {
            int itemCount = supp.Count();

            if (itemCount <= 10)
            {
                return itemCount == 10 ? "Major" : "Minor";
            }
            else
            {
                return "Critical";
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
