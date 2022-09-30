using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private static readonly List<Book> _testModels = new List<Book>()
     {
        new Book()
        {
            Id = 1,
            Title = "Book 1",
            AuthorId = 1
        },
        new Book()
        {
            Id = 2,
            Title = "Book2",
            AuthorId = 2
        } };

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }


        [HttpGet(nameof(Get))]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            return _bookService.GetById(id);
        }

        [HttpPost(nameof(AddModel))]
        public Book? AddModel([FromBody] Book book)
        {
            return _bookService.AddBook(book);
        }

        [HttpPut(nameof(AddModel))]
        public Book? Update([FromBody] Book book)
        {
            return _bookService.UpdateBook(book);
        }
        [HttpDelete(nameof(AddModel))]
        public Book? Delete(int id)
        {
            return _bookService.DeleteBook(id);
        }

    }
}
