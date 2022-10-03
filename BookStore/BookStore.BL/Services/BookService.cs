using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using System.Net;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookInMemoryRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookInMemoryRepository)
        {
            _bookInMemoryRepository = bookInMemoryRepository;
        }
        public Book? AddBook(Book book)
        {
            return _bookInMemoryRepository.AddBook(book);
        }

        public BookResponse AddBook(BookRequest bookRequest)
        {
            var book = _bookInMemoryRepository.GetById(bookRequest.Id);

            if (book != null)
                return new BookResponse()
                {
                    Book = book,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book already exist"
                };

            if (bookRequest.Id <= 0)
            {
                return new BookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Parameter id:{bookRequest.Id} must be greater than 0"
                };
            }
            var b = _mapper.Map<Book>(bookRequest);
            var result = _bookInMemoryRepository.AddBook(_mapper.Map<Book>(b));

            return new BookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result,
                Message = "Successfully added book"
            };
        }

        public Book? DeleteBook(int bookId)
        {
            return _bookInMemoryRepository.DeleteBook(bookId);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookInMemoryRepository.GetAllBooks();
        }

        public Book? GetById(int id)
        {
            return _bookInMemoryRepository.GetById(id);
        }

        public Book UpdateBook(Book book)
        {
            return _bookInMemoryRepository.UpdateBook(book);
        }

        public BookResponse UpdateBook(BookRequest bookRequest)
        {

            var book = _bookInMemoryRepository.GetById(bookRequest.Id);

            if (book == null)
                return new BookResponse()
                {
                    Book = book,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book does not exist"
                };

            var b = _mapper.Map<Book>(book);
            var result = _bookInMemoryRepository.UpdateBook(b);

            return new BookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result,
                Message = "Successfully updated book"
            };
        }
    }
}
