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
        private readonly DbService _dbService;
        private readonly ILogger<OrderController> _logger;

        public SaleController(DbService dbService, ILogger<OrderController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        // GET: api/<SaleController>
        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            return _dbService.GetAllSale();
        }

        // GET api/<SaleController>/5
        [HttpGet("{ProductName}")]
        public IEnumerable<Sale> Get(string ProductName)
        {
            return _dbService.GetAllSale().Where(sale => sale.ProductName == ProductName);
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

                var rawResult = _dbService.GetAllSale()
                   .Where(sale => sale.SaleDate.Year == year && sale.SaleDate.Month <= 3)
                   .GroupBy(group => new { group.ProductName })
                   .Select(customergroup => new
                   {
                       Product = customergroup.Key.ProductName,
                       TotalAmnt = customergroup.Sum(group => group.QuantitySold)
                   }).Take(3);

                if (rawResult.Any())
                {
                    return Ok(new { ProductWithHighQuantityFirst3Month = rawResult });
                }

                return NotFound(new { message = "no data found for year {year} " });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaleController:Method:GetTop3ProductsWithHighSalesForFirst3MonthsOfYear:Error {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        //Get  api/<Sale>/month/2023
        [HttpGet("year/{year}/month/{month}")]
        public ActionResult<IEnumerable<object>> GetTotalSalesOfYearForMonth(int year, int month)
        {
            _logger.LogInformation("SaleController:Method:GetTotalSalesOfYearForMonth Called.");
            try
            {

                if (year <= 0 || month <= 0)
                {
                    return BadRequest();
                }

                var rawResult = _dbService.GetAllSale()
                                .Where(sale => sale.SaleDate.Year == year && sale.SaleDate.Month == month)
                                .Sum(quantity => quantity.QuantitySold);


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

                // var fromDate = new DateTime(year, 12, 31).AddMonths(-6);
                var fromDate = DateTime.Now.AddMonths(-6);

                var rawResult = _dbService.GetAllSale()
                                   .Where(sale => sale.SaleDate.Year == year && sale.SaleDate > fromDate)
                                   .GroupBy(gr => gr.ProductName)
                                   .Select(sales => new
                                   {
                                       Product = sales.Key,
                                       TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                                   })
                                   .OrderByDescending(pop => pop.TotalAmnt).First();


                if (rawResult is not null) { return Ok(new { rawResult }); }

                return NotFound(new { message = " no data found for 6 month of year:{year}.", year });
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

                // var fromDate = new DateTime(year, 12, 31).AddMonths(-6);

                var fromDate = DateTime.Now.AddMonths(-6);

                var rawResult = _dbService.GetAllSale()
                                   .Where(sale => sale.SaleDate.Year == year && sale.SaleDate > fromDate)
                                   .GroupBy(gr => gr.ProductName)
                                   .Select(sales => new
                                   {
                                       Product = sales.Key,
                                       TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                                   })
                                   .OrderBy(pop => pop.TotalAmnt).First();

                if (rawResult is not null) { return Ok(new { rawResult }); }

                return NotFound(new { message = " no data found for 6 month of year:{year}.", year });
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

                var rawResult = _dbService.GetAllSale()
                                 .Where(sale => sale.SaleDate.Year == year)
                                 .GroupBy(gr => gr.ProductName)
                                 .Select(sales => new
                                 {
                                     Product = sales.Key,
                                     TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                                 })
                                 .OrderByDescending(pop => pop.TotalAmnt).First();

                if (rawResult is not null)
                { return Ok(rawResult); }


                return NotFound();
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


                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Product = group.ProductName
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Product = sold.Key.Product,
                                  Sold = sold.Sum(sold => sold.QuantitySold)
                              })
                              .OrderBy(sold => sold.Sold).First();

                if (rawResult is not null)
                { return Ok(rawResult); }


                return NotFound();
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

                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                              });

                if (rawResult is not null)
                { return Ok(rawResult); }

                return NotFound();
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

                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Month = group.SaleDate.Month,
                                  Product = group.ProductName
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Month = sold.Key.Month,
                                  Product = sold.Key.Product,
                                  TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                              })
                              .GroupBy(group2 => new
                              {
                                  group2.Year,
                                  group2.Month
                              })
                              .Select(result => result
                                               .OrderByDescending(ord2 => ord2.TotalQuantity).First());


                if (rawResult is not null)
                { return Ok(rawResult); }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetMostSoldProductMonthWiseOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET /api/<Sale>/year/2023
        [HttpGet("GetProductGroupedBySeason/Top/{top}")]
        public ActionResult<IEnumerable<object>> GetProductGroupedBySeason(int top)
        {
            _logger.LogInformation("PurchaseController:Method:GetProductGroupedBySeason Called");

            try
            {
                if (top <= 0)
                {
                    var rawResult = _dbService.GetAllSale()
                                    .GroupBy(group => Helpers.GetSeason(group.SaleDate))
                                    .Select(result => new
                                    {
                                        Season = result.Key,
                                        ProductSales = result.GroupBy(group1 => group1.ProductName)
                                        .Select(newresult => new
                                        {
                                            Productname = newresult.Key,
                                            TotalQuantity = newresult.Sum(quan => quan.QuantitySold)
                                        })
                                    });

                    if (rawResult.Any())
                        return Ok(rawResult);
                }
                else
                {
                    var rawResult = _dbService.GetAllSale()
                                    .GroupBy(group => Helpers.GetSeason(group.SaleDate))
                                    .Select(result => new
                                    {
                                        Season = result.Key,
                                        ProductSales = result.GroupBy(group1 => group1.ProductName)
                                        .Select(newresult => new
                                        {
                                            Productname = newresult.Key,
                                            TotalQuantity = newresult.Sum(quan => quan.QuantitySold)
                                        })
                                        .OrderByDescending(result => result.TotalQuantity)
                                        .Take(top)
                                    });

                    if (rawResult.Any())
                        return Ok(rawResult);
                }

                return NotFound("No Data Found");
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

                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Month = group.SaleDate.Month
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Month = sold.Key.Month,
                                  TotalAverage = sold.Average(sold => sold.QuantitySold)
                              });

                if (rawResult is not null)
                { return Ok(rawResult); }

                return NotFound();
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


                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Product = group.ProductName
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Product = sold.Key.Product,
                                  TotalSum = sold.Sum(sold => sold.QuantitySold),
                                  TotalAverage = sold.Average(sold => sold.QuantitySold),
                                  Min = sold.Min(sold => sold.QuantitySold),
                                  Max = sold.Max(sold => sold.QuantitySold),
                                  Count = sold.Count()
                              });

                if (rawResult is not null)
                { return Ok(rawResult); }

                return NotFound();
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

                var rawResult = _dbService.GetAllSale()
                              .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Month = group.SaleDate.Month,
                                  Product = group.ProductName
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Month = sold.Key.Month,
                                  Product = sold.Key.Product,
                                  TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                              })
                              .GroupBy(group2 => new
                              {
                                  group2.Year,
                                  group2.Month
                              })
                              .Select(result => result
                                               .OrderBy(ord2 => ord2.TotalQuantity).First());

                if (rawResult is not null)
                { return Ok(rawResult); }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("PurchaseController:Method:GetLeastSoldProductMonthWiseOfYear: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //// POST api/<SaleController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SaleController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SaleController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
