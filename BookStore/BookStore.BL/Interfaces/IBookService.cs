using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        BookResponse AddBook (BookRequest bookRequest);
        BookResponse UpdateBook (BookRequest bookRequest);
        Book? DeleteBook(int bookId);
    }
}
