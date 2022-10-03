using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class BookInMemoryRepository : IBookRepository
    {
        private static List<Book> _books = new List<Book>()
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
        public Book? AddBook(Book book)
        {
             try
            {
                _books.Add(book);
            }
            catch (Exception e)
            {
                return null;
            }

            return book;
        }

        public Book? DeleteBook(int bookId)
        {
            if (bookId <= 0) return null;

            var book = _books.FirstOrDefault(x => x.Id == bookId);

            _books.Remove(book);

            return book;
        }

        public IEnumerable<Book> GetAllBooks() => _books;

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public Book UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == book.Id);

            if (existingBook == null) return null;

            _books.Remove(existingBook);

            _books.Add(book);

            return book;
        }
    }
}
