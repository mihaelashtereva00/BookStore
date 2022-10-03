using BookStore.BL.Interfaces;
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

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllUsers();
        }

        [HttpGet(nameof(GetById))]
        public Person? GetById(int id)
        {
            return _personService.GetById(id);
        }

        [HttpPost(nameof(AddModel))]
        public Person? AddModel([FromBody] Person user)
        { 
         return _personService.AddUser(user);
        }

        [HttpPut(nameof(AddModel))]
        public Person? Update([FromBody] Person user)
        {
            return _personService.UpdateUser(user);
        }
        [HttpDelete(nameof(AddModel))]
        public Person? Delete(int id)
        {
            return _personService.DeleteUser(id);
        }

    }
}