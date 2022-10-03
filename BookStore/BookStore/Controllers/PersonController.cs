using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        private static readonly List<Person> _testModels = new List<Person>()
        {
            new Person()
            {
            Id = 1,
            Name = "Name1"
            },
            new Person()
            {
            Id = 2,
            Name = "Name2"
            }
        };

        public PersonController(ILogger<PersonController> logger, IPersonService userInmemoryRepository)
        {
            _logger = logger;
            _personService = userInmemoryRepository;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {

            var result = _personService.GetAllUsers();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest($"Parameter id:{id} must be greater than 0");
            var result = _personService.GetById(id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(AddModel))]
        public IActionResult AddModel([FromBody] Person user)
        {
            if (user == null) return BadRequest($"User can not be null");
            _personService.AddUser(user);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(AddModel))]
        public IActionResult Update([FromBody] Person user)
        {
            if (user == null) return BadRequest($"User can not be null");
            _personService.UpdateUser(user);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (id > 0 && _personService.GetById(id) != null)
            {
                _personService.DeleteUser(id);
                return Ok();

            }
            return BadRequest();
        }
    }
}