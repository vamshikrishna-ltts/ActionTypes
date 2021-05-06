using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiCRUD.EmployeeData;
using RestApiCRUD.Models;
namespace RestApiCRUD.Controllers
{   
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;
       public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

      

        //specific type example
       [HttpGet]
       [Route("api/[controller]")]

       public List<Employee> getListOfEmployees()
        {
            _employeeData.GetEmployees();
        }
         [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }


        //synchronous IActionResult
        [HttpGet]
        [Route("api/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
         [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if(employee != null)
            {
                return Ok(employee);

            }
            return NotFound("No data id not found");
        }


        //Asynchronous IActionResult
        [HttpPost]
        [Route("api/[controller]")]
        [Consumes(MediaTypeNames.Application.Json)]
         [ProducesResponseType(StatusCodes.Status201Created)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            if (employee.Name.Contains("unknown"))
            {
                return BadRequest();
            }

           await _employeeData.AddNewEmployee(employee);

             return Created(HttpContext.Request.Scheme + "://" + 
                HttpContext.Request.Host + "/" + employee.Id, employee); 
           
        }
    }
}
