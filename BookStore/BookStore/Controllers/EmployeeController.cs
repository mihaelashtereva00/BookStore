using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add employee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get all")]
        public async Task<IActionResult> Get()
        {
           // await _employeeService.GetEmployeeDetails();
            return Ok(await _employeeService.GetEmployeeDetails());
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            
            return Ok(await _employeeService.GetEmployeeDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }

    }
}
