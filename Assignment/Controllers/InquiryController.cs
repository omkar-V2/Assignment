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
        private readonly DbService _dbService;
        private readonly ILogger<InquiryController> _logger;

        public InquiryController(DbService dbService, ILogger<InquiryController> logger)
        {
            _dbService = dbService;
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
                var fromDate = _dbService.GetAllInquiry().Max(inq => inq.InquiryDate).AddMonths(-3);

                var rawResult = _dbService.GetAllInquiry()
                                 .Where(get => get.InquiryDate > fromDate)
                                 .GroupBy(group => new
                                 {
                                     product = group.CategoryName
                                 })
                                 .Select(inq1 => new
                                 {
                                     inq1.Key.product,
                                     Quantity = inq1.Max(quant => quant.Quantity)
                                 })
                                 .OrderByDescending(inq => inq.Quantity);


                if (rawResult.Any())
                    return Ok(new
                    {
                        rawResult
                    });


                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryController:Method:GetProductCategoryWithHighestNoOfInquiryInLast3Month Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }

        }

        // GET api/<InquiryController>
        [HttpGet("GetAllCategoryTotalNoOfInquiryInLast3Month")]
        public ActionResult<IEnumerable<object>> GetAllCategoryTotalNoOfInquiryInLast3Month()
        {
            _logger.LogInformation("InquiryController:Method:GetAllCategoryTotalNoOfInquiryInLast3Month called.");
            try
            {
                var fromDate = _dbService.GetAllInquiry().Max(inq => inq.InquiryDate).AddMonths(-3);

                var rawResult = _dbService.GetAllInquiry()
                                 .Where(get => get.InquiryDate > fromDate)
                                .GroupBy(group => new
                                {
                                    product = group.CategoryName
                                })
                                .Select(inq1 => new
                                {
                                    category = inq1.Key.product,
                                    Quantity = inq1.Sum(quant => quant.Quantity)
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
                _logger.LogError("InquiryController:Method:GetAllCategoryTotalNoOfInquiryInLast3Month Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }

        }

        // GET api/<InquiryController>
        [HttpGet("GetDateWithHighestNoOfInquiryInYear/{year}")]
        public ActionResult<IEnumerable<object>> GetDateWithHighestNoOfInquiryInYear(int year)
        {
            _logger.LogInformation("InquiryController:Method:GetDateWithHighestNoOfInquiryInYear called.");
            try
            {
                var fromDate = _dbService.GetAllInquiry().Max(inq => inq.InquiryDate).AddMonths(-3);

                var rawResult = _dbService.GetAllInquiry()
                                .Where(get => get.InquiryDate.Year == year)
                                .GroupBy(group => new
                                {
                                    InqDate = group.InquiryDate.Date
                                })
                                .Select(inq1 => new
                                {
                                    Date = inq1.Key.InqDate,
                                    QuantityNoOfinquiry = inq1.Max(high => high.Quantity)
                                })
                                .GroupBy(group => new
                                {
                                    inqcount = group.QuantityNoOfinquiry
                                })
                                .MaxBy(inq1 => inq1.Key.inqcount); ;

                if (rawResult != null)
                    return Ok(new
                    {
                        rawResult
                    });


                return NotFound(new { message = "No data found." });
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryController:Method:GetDateWithHighestNoOfInquiryInYear Error: {ex}", ex);

                var json = JsonSerializer.Serialize(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, json);
            }

        }


        // GET api/<InquiryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<InquiryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<InquiryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<InquiryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
