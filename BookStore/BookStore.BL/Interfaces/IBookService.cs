using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetById(int id);
        Task<BookResponse> AddBook (BookRequest bookRequest);
        Task<BookResponse> UpdateBook (BookRequest bookRequest);
        Task<Book?> DeleteBook(int bookId);
    }
}
