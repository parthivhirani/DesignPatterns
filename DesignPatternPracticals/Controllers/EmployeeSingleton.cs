using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatternPracticals.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class EmployeeSingleton : Controller
    {
        private readonly IDataAccessService _dataAccessService;
        private readonly ILogger<EmployeeSingleton> _logger;

        public EmployeeSingleton(IDataAccessService dataAccessService,
                                    ILogger<EmployeeSingleton> logger)
        {
            _dataAccessService = dataAccessService;
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Get(int? id)
        {
            var empList = _dataAccessService.GetAllEmployees(id);
            if(empList != null)
            {
                _logger.LogInformation($"Employee details retrieved with id = {id}");
                return Ok(empList);
            }
            _logger.LogError($"Can't get employee with id = {id}");
            return NotFound();
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public IActionResult Create(Employee employee)
        {
            var addResult = _dataAccessService.CreateEmployee(employee);
            if(addResult == true)
            {
                _logger.LogInformation($"Employee details added with name = {employee.Name}");
                return Ok("Employee added successfully");
            }
            _logger.LogInformation($"Employee details can't be added with name = {employee.Name}");
            return BadRequest("There is some error while inserting employee");
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public IActionResult Edit(int id, Employee employee)
        {
            var editResult = _dataAccessService.EditEmployee(id, employee);
            if (editResult == true)
            {
                _logger.LogInformation($"Employee details edited with id = {id}");
                return Ok("Employee edited successfully");
            }
            _logger.LogInformation($"Employee details can't be edited with id = {id}");
            return BadRequest("Employee can't be edited");
        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        public IActionResult Delete(int id)
        {
            var deleteResult = _dataAccessService.DeleteEmployee(id);
            if (deleteResult == true)
            {
                _logger.LogInformation($"Employee details deleted with id = {id}");
                return Ok("Employee deleted successfully");
            }
            _logger.LogInformation($"Employee details can't be deleted with id = {id}");
            return BadRequest("Employee can't be deleted");
        }
    }
}
