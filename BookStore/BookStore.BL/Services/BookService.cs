using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookInMemoryRepository _bookInMemoryRepository;
        public BookService(IBookInMemoryRepository bookInMemoryRepository)
        {
            _bookInMemoryRepository = bookInMemoryRepository;
        }
        public Book? AddBook(Book book)
        {
            return _bookInMemoryRepository.AddBook(book);
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
    }
}
