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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
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
            await _employeeService.GetEmployeeDetails();
            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            await _employeeService.GetEmployeeDetails(id);
            return Ok();
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
