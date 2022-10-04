using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetById(int id);
        Task<Book?> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book?> DeleteBook(int bookId);
    }
}
