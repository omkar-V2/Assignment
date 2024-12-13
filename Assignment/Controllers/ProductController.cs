using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ExternalApiService _externalApiService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, IProductService productService, ExternalApiService externalApiService)
        {
            _logger = logger;
            _productService = productService;
            _externalApiService = externalApiService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<ProductController>/partner/1
        [HttpGet("partner/{limit}")]
        public async Task<ActionResult<IEnumerable<ProductExternal>>> GetProductsAsync(string limit)
        {
            try
            {
                var result = await _externalApiService.GetProductsAsync(limit);

                if (result?.Any() == true)
                {
                    return Ok(result);
                }

                return Ok(Enumerable.Empty<ProductExternal>());
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController:GetProductsAsync Error:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET: api/<ProductController>/category/jewelery
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryExternal>>> GetProductsByCategoryAsync(string category)
        {
            try
            {
                var result = await _productService.GetProductsByCategoryAsync(category);

                if (result?.Any() == true)
                {
                    return Ok(result);
                }

                return Ok(Enumerable.Empty<ProductCategoryExternal>());
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController:GetProductsAsync Error:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }


}
