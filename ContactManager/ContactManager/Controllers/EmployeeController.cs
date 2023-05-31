using ContactManager.Models;
using ContactManager.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult UploadCsv(IFormFile file)
        {
            _employeeService.ReadEmployeesFromCsv(file);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpDelete]
        public IActionResult DeleteEmployees(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpPut]

        public IActionResult EditEmployee(Employee employee)
        {
            _employeeService.EditEmployee(employee);
            return Ok();
        }
    }
}
