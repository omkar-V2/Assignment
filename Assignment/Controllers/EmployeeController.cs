using System.Net.Mime;
using Assignment;
using Common;
using EmployeeManagement.Model;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMPreparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/<EmployeeController>        
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<Employee>), contentType: ContentTypes.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            _logger.LogInformation("EmployeeController Get Method Called");
            try
            {
                var employees = _employeeService.GetAll();
                if (employees.Any())
                {
                    return Ok(employees);
                }
                return NotFound(new { message = $"No employee found." });

            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeController Get Method Called:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmployee(int id)
        {
            _logger.LogInformation("EmployeeController GetEmployee by id Method Called");

            try
            {
                var employees = _employeeService.SearchEmployee(id);
                if (employees.Any())
                {
                    return Ok(employees);
                }
                return NotFound(new { message = $"No employee found for Id;{id}." });

            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeController GetEmployee by id Method Called:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Employee>> Search(string firstName)
        {
            _logger.LogInformation("EmployeeController Search Method Called");

            try
            {
                var employees = _employeeService.SearchEmployee(firstName);

                if (employees.Any())
                {
                    return Ok(employees);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeController Search Method Called:{ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // POST api/<EmployeeController>
        [HttpPost]
        public ActionResult Post([FromBody] Employee employee)
        {
            _logger.LogInformation("EmployeeController Add Method Called");

            try
            {
                var result = _employeeService.Add(employee);

                if (result != null)
                {
                    return CreatedAtAction("", new { id = result.Id }, result);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Employee employee)
        {
            _logger.LogInformation("EmployeeController Update Method Called");

            try
            {
                var employees = _employeeService.SearchEmployee(id).FirstOrDefault();

                if (employees is null)
                {
                    return BadRequest("Something is wrong. Kindly check with Administrator.");
                }
                else if (employees is not null && id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                var result = _employeeService.Update(employee);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("EmployeeController Delete Method Called");
            try
            {
                var employee = _employeeService.SearchEmployee(id).FirstOrDefault();

                if (employee is null)
                {
                    return BadRequest("Something is wrong. Kindly check with Administrator.");
                }
                else if (employee is not null && id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                var result = _employeeService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
