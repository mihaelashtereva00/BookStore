using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get all")]
        public IActionResult Get()
        {
            var result = _bookService.GetAllBooks();
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
            var result = _bookService.GetById(id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Add book")]
        public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequest)
        {
            return Ok(await _bookService.AddBook(bookRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Update book")]
        public async Task<IActionResult> Update(BookRequest bookRequest)
        {
            return Ok(await _bookService.UpdateBook(bookRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete book")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0 && _bookService.GetById != null)
            {
                return Ok(await _bookService.DeleteBook(id));
            }
            return BadRequest();
        }

    }
}
