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
        public readonly ExternalApiService _externalApiService;
        public readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, ExternalApiService externalApiService)
        {
            _logger = logger;
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
        public async Task<ActionResult<List<ProductExternal>>> GetProductsAsync(string limit)
        {
            try
            {
                string uri = $"https://fakestoreapi.com/products?limit={limit}";

                var responseResult = await _externalApiService.GetProductsAsync(uri);

                var jsonObject = JsonSerializer.Deserialize<List<ProductExternal>>(responseResult);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController:GetProductsAsync Error:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }


    public class ProductExternal
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        // Add other properties as needed
    }

}
