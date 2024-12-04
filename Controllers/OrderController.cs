using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

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
