using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSql
{
    public class BookRepository : IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IConfiguration _configuration;
        public BookRepository(ILogger<BookRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Book?> AddBook(Book book)
        {

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Books (AuthorId, Title, LastUpdated, Quantity, Price) VALUES ( @AuthorId, @Title, @LastUpdated, @Quantity, @Price)";
                    var resul = conn.ExecuteAsync(query, book);
                    return book;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add book");
                return null;
            }
        }

        public Task<Book?> DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
