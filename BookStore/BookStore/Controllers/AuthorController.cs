using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        private static readonly List<Author> _testModels = new List<Author>()
        {
            new Author()
            {
            Id = 1,
            Name = "Name1"
            },
            new Author()
            {
            Id = 2,
            Name = "Name2"
            }
        };

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       [HttpPost("Add author")]
       public IActionResult AddAuthor([FromBody] AuthorRequest addAuthorRequest)
        {
            var result = _authorService.AddAuthor(addAuthorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get all")]
        public IActionResult Get()
        {
            var result = _authorService.GetAllAuthors();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest($"Parameter id:{id} must be greater than 0");
            var result = _authorService.GetById(id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetByName))]
        public IActionResult GetByName(string name)
        {
            if (name.Length <= 0) return BadRequest($"Parameter name:{name} must be greater than 0");
            var result = _authorService.GetByName(name);
            if (result == null) return NotFound();
            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Update author")]
        public IActionResult Update(AuthorRequest updateAuthorRequest)
        {
            var result = _authorService.UpdateAuthor(updateAuthorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int id)
        {
            if (id > 0 && _authorService.GetById(id) != null)
            {
                _authorService.DeleteAuthor(id);
                return Ok();

            }
            return BadRequest();
        }


    }
}
