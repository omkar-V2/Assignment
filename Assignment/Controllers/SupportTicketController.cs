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
        private readonly IDbSupportTicketService _dbSupportTicketService;
        private readonly ILogger<SupportTicketController> _logger;

        public SupportTicketController(IDbSupportTicketService dbSupportTicketService, ILogger<SupportTicketController> logger)
        {
            _dbSupportTicketService = dbSupportTicketService;
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
                var rawResult = _dbSupportTicketService.GetDuplicateSupportActivity();

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetDuplicateSupportActivity Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsAverageInLast3Month")]
        public ActionResult<object> GetSupportTicketsAverageInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsAverageInLast3Month called.");
            try
            {
                var rawResult = _dbSupportTicketService.GetSupportTicketsAverageInLast3Month();

                return Ok(new
                {
                    message = $"Support Tickets Average In Last 3 Month:{rawResult}"
                });

                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsAverageInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsTotalNoPerCategoryInLast3Month")]
        public ActionResult<object> GetSupportTicketsTotalNoPerCategoryInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsTotalNoPerCategoryInLast3Month called.");
            try
            {

                var rawResult = _dbSupportTicketService.GetSupportTicketsTotalNoPerCategoryInLast3Month();

                if (rawResult.Any())
                    return Ok(new { rawResult });

                return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsTotalNoPerCategoryInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsTotalNoPerMonthInLast3Month")]
        public ActionResult<object> GetSupportTicketsTotalInLast3Month()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsTotalNoPerMonthInLast3Month called.");
            try
            {
                var rawResult = _dbSupportTicketService.GetSupportTicketsTotalInLast3Month();

                return Ok(new
                {
                    rawResult
                });

            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsTotalNoPerMonthInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<SupportTicketController>
        [HttpGet("GetSupportTicketsAveragePerMonth")]
        public ActionResult<object> GetSupportTicketsAveragePerMonth()
        {
            _logger.LogInformation("SupportTicketController:Method:GetSupportTicketsAveragePerMonth called.");
            try
            {
                var rawResult = _dbSupportTicketService.GetSupportTicketsAveragePerMonth();

                return Ok(new
                {
                    message = $"Support Tickets Average Per Month:{rawResult}"
                });

                //return NotFound(new { message = "No data found for customer." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetSupportTicketsAveragePerMonth Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //GEt api/<SupportTicketController>/GetMajorSupportTicketCountAndStatus
        [HttpGet("GetMajorSupportTicketCountAndStatus")]
        public ActionResult<IEnumerable<object>> GetMajorSupportTicketCountAndStatus()
        {
            _logger.LogInformation("SupportTicketController:Method:GetMajorSupportTicketCountAndStatus called.");
            try
            {
                var rawResult = _dbSupportTicketService.GetMajorSupportTicketCountAndStatus();

                if (rawResult.Any())
                    return Ok(new
                    {
                        rawResult = rawResult,
                        // message = $"Support Tickets Average Per Month:{result}"
                    });

                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetMajorSupportTicketCountAndStatus Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        //GET api/<SupportTicketController>/GetHighestNoSupportTicketsMonthOfYear
        [HttpGet("GetHighestNoSupportTicketsMonthOfYear/{year}")]
        public ActionResult<IEnumerable<object>> GetHighestNoSupportTicketsMonthOfYear(int year)
        {
            _logger.LogInformation("SupportTicketController:Method:GetHighestNoSupportTicketsMonthOfYear called.");
            try
            {
                var rawResult = _dbSupportTicketService.GetHighestNoSupportTicketsMonthOfYear(year);

                if (rawResult != null)
                    return Ok(new { MonthWithHighestNoOfSupportTickets = rawResult });

                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("SupportTicketController:Method:GetHighestNoSupportTicketsMonthOfYear Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}
