using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet(nameof(Get))]
        public IEnumerable<Author> Get()
        {
            return _authorService.GetAllAuthors();
        }

        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            return _authorService.GetById(id);
        }

        [HttpPost(nameof(AddModel))]
        public Author? AddModel([FromBody] Author author)
        {
            return _authorService.AddAuthor(author);
        }

        [HttpPut(nameof(AddModel))]
        public Author? Update([FromBody] Author author)
        {
            return _authorService.UpdateAuthor(author);
        }
        [HttpDelete(nameof(AddModel))]
        public Author? Delete(int id)
        {
            return _authorService.DeleteAuthor(id);
        }

    }
}
