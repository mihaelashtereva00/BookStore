using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        Book? AddBook(Book book);
        Book UpdateBook(Book book);
        Book? DeleteBook(int bookId);
    }
}
