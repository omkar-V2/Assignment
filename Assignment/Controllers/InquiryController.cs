using System.Text.Json;
using CCMPreparation.Controllers;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using static CCMPreparation.Controllers.OrderController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IDbInquiryService _dbInquiryService;
        private readonly ILogger<InquiryController> _logger;

        public InquiryController(IDbInquiryService dbInquiryService, ILogger<InquiryController> logger)
        {
            _dbInquiryService = dbInquiryService;
            _logger = logger;
        }


        // GET: api/<InquiryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InquiryController>
        [HttpGet("GetProductCategoryWithHighestNoOfInquiryInLast3Month")]
        public ActionResult<IEnumerable<object>> GetProductCategoryWithHighestNoOfInquiryInLast3Month()
        {
            _logger.LogInformation("InquiryController:Method:GetProductCategoryWithHighestNoOfInquiryInLast3Month called.");
            try
            {
                var rawResult = _dbInquiryService.GetProductCategoryWithHighestNoOfInquiryInLast3Month();

                if (rawResult != null)
                    return Ok(new
                    {
                        rawResult
                    });


                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryController:Method:GetProductCategoryWithHighestNoOfInquiryInLast3Month Error: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // GET api/<InquiryController>
        [HttpGet("GetAllCategoryTotalNoOfInquiryInLast3Month")]
        public ActionResult<IEnumerable<object>> GetAllCategoryTotalNoOfInquiryInLast3Month()
        {
            _logger.LogInformation("InquiryController:Method:GetAllCategoryTotalNoOfInquiryInLast3Month called.");
            try
            {

                var rawResult = _dbInquiryService.GetAllCategoryTotalNoOfInquiryInLast3Month();

                if (rawResult.Any())
                    return Ok(new
                    {
                        rawResult
                    });


                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryController:Method:GetAllCategoryTotalNoOfInquiryInLast3Month Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // GET api/<InquiryController>
        [HttpGet("GetDateWithHighestNoOfInquiryInYear/{year}")]
        public ActionResult<object> GetDateWithHighestNoOfInquiryInYear(int year)
        {
            _logger.LogInformation("InquiryController:Method:GetDateWithHighestNoOfInquiryInYear called.");
            try
            {
                var rawResult = _dbInquiryService.GetDateWithHighestNoOfInquiryInYear(year);

                if (rawResult != null)
                    return Ok(new
                    {
                        rawResult
                    });
                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryController:Method:GetDateWithHighestNoOfInquiryInYear Error: {ex}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

    }
}
