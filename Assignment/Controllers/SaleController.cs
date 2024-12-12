using System.Collections;
using System.Reflection;
using System.Text;
using Assignment.Controllers;
using Common;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IDbSaleService _dbSaleService;
        private readonly ILogger<OrderController> _logger;

        public SaleController(IDbSaleService dbSaleService, ILogger<OrderController> logger)
        {
            _dbSaleService = dbSaleService;
            _logger = logger;
        }

        // GET: api/<SaleController>
        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            return _dbSaleService.GetAllMonthlySales();
        }

        // GET api/<SaleController>/5
        [HttpGet("{ProductName}")]
        public IEnumerable<Sale> Get(string ProductName)
        {
            return _dbSaleService.GetProductMonthlySales(ProductName);
        }


        // GET api/<Sale>/month/2023
        [HttpGet("GetTop3ProductsWithHighSalesForFirst3MonthsOfYear/{year}")]
        public ActionResult<IEnumerable<object>> GetTop3ProductsWithHighSalesForFirst3MonthsOfYear(int year)
        {
            _logger.LogInformation("SaleController:Method:GetTop3ProductsWithHighSalesForFirst3MonthsOfYear Called.");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetTop3ProductsWithHighSalesForFirst3MonthsOfYear(year);

                if (rawResult.Any())
                {
                    return Ok(new { ProductWithHighQuantityFirst3Month = rawResult });
                }

                return Ok(Enumerable.Empty<object>());

                //return NotFound(new { message = "no data found for year {year} " });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaleController:Method:GetTop3ProductsWithHighSalesForFirst3MonthsOfYear:Error {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        //Get  api/<Sale>/month/2023
        [HttpGet("TotalOf/year/{year}/month/{month}")]
        public ActionResult<IEnumerable<object>> GetTotalSalesOfYearForMonth(int year, int month)
        {
            _logger.LogInformation("SaleController:Method:GetTotalSalesOfYearForMonth Called.");
            try
            {

                if (year <= 0 || month <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetTotalSalesOfYearForMonth(year, month);

                return Ok($"Total Sales Of Year {year} For Month {month} is:{rawResult}");

                // return NotFound(new { message = "no data found for year {year} and month{month}" });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaleController:Method:GetTotalSalesOfYearForMonth:Error {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get  api/<Sale>/month/2023
        [HttpGet("GetMostPopularProductInLast6MonthOfYear/{year}")]
        public ActionResult<IEnumerable<object>> GetMostPopularProductInLast6MonthOfYear(int year)
        {
            _logger.LogInformation("SaleController:Method:GetMostPopularProductInLast6MonthOfYear called.");
            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetMostPopularProductInLast6MonthOfYear(year);

                if (rawResult is not null) { return Ok(new { rawResult }); }

                return Ok(Enumerable.Empty<object>());
                //return NotFound(new { message = " no data found for 6 month of year:{year}.", year });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaleController:Method:GetMostPopularProductInLast6MonthOfYear:Error {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get  api/<Sale>/month/2023
        [HttpGet("GetLeastPopularProductInLast6MonthOfYear/{year}")]
        public ActionResult<IEnumerable<object>> GetLeastPopularProductInLast6MonthOfYear(int year)
        {
            _logger.LogInformation("SaleController:Method:GetLeastPopularProductInLast6MonthOfYear called.");
            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetLeastPopularProductInLast6MonthOfYear(year);

                if (rawResult is not null) { return Ok(new { rawResult }); }

                return Ok(Enumerable.Empty<object>());
                //  return NotFound(new { message = " no data found for 6 month of year:{year}.", year });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaleController:Method:GetLeastPopularProductInLast6MonthOfYear:Error {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetMostSoldProductByYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetMostSoldProductByYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetMostSoldProductByYear Called");

            try

            {
                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetMostSoldProductByYear(year);

                if (rawResult is not null)
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                //return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetMostSoldProductByYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetLeastSoldProductByYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetLeastSoldProductByYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetLeastSoldProductByYear Called");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetLeastSoldProductByYear(year);

                if (rawResult is not null)
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                // return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetLeastSoldProductByYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetTotalProductQuantitySoldByYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetTotalProductQuantitySoldByYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetTotalProductQuantitySoldByYear Called");

            try
            {
                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetTotalProductQuantitySoldByYear(year);

                if (rawResult is not null)
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                // return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetTotalProductQuantitySoldByYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetMostSoldProductMonthWiseOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetMostSoldProductMonthWiseOfYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetMostSoldProductMonthWiseOfYear Called");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetMostSoldProductMonthWiseOfYear(year);

                if (rawResult is not null)
                { return Ok(rawResult); }

                // return NotFound();
                return Ok(Enumerable.Empty<object>());
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetMostSoldProductMonthWiseOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetProductGroupedBySeason/Top/{top}")]
        public ActionResult<Dictionary<string, IEnumerable<object>>> GetProductGroupedBySeason(int top)
        {
            _logger.LogInformation("PurchaseController:Method:GetProductGroupedBySeason Called");

            try
            {
                var rawResult = _dbSaleService.GetProductGroupedBySeason(top);

                if (rawResult.Any())
                {
                    return Ok(rawResult);
                }

                return Ok(Enumerable.Empty<Dictionary<string, IEnumerable<object>>>());
                //  return NotFound("No Data Found");
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetProductGroupedBySeason: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetAveargeQuanOfProductSoldMonthWiseOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetAveargeQuanOfProductSoldMonthWiseOfYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetAveargeQuanOfProductSoldMonthWiseOfYear Called");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetAveargeQuanOfProductSoldMonthWiseOfYear(year);

                if (rawResult.Any())
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                //return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetAveargeQuanOfProductSoldMonthWiseOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetAggregateOfEachProductSoldOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetAggregateOfEachProductSoldOfYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetAggregateOfEachProductSoldOfYear Called");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }


                var rawResult = _dbSaleService.GetAggregateOfEachProductSoldOfYear(year);

                if (rawResult is not null)
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                // return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetAggregateOfEachProductSoldOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetLeastSoldProductMonthWiseOfYear/year/{year}")]
        public ActionResult<IEnumerable<object>> GetLeastSoldProductMonthWiseOfYear(int year)
        {
            _logger.LogInformation("PurchaseController:Method:GetLeastSoldProductMonthWiseOfYear Called");

            try
            {

                if (year <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbSaleService.GetLeastSoldProductMonthWiseOfYear(year);

                if (rawResult.Any())
                { return Ok(rawResult); }

                return Ok(Enumerable.Empty<object>());
                // return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetLeastSoldProductMonthWiseOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
