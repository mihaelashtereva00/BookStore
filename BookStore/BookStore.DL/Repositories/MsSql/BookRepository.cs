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
                    _logger.LogInformation("Book added");
                    return book;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add book");
                return null;
            }
        }

        public async Task<Book?> DeleteBook(int bookId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Book>("DELETE FROM Books WHERE Id = @Id", new { Id = bookId });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not delete book");
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Book>("SELECT * FROM Books WITH(NOLOCK)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)} - {ex.Message}", ex);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)} - {ex.Message}", ex);
            }
            return new Book();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE Books SET AuthorId = @AuthorId, Title = @Title, LastUpdated = @LastUpdated, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, book);
                    return book;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateBook)} - {ex.Message}", ex);
            }
            return null;
        }

        public async Task<bool> DoesAuthorExist(int id)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var auhtor = await conn.QueryMultipleAsync("SELECT * FROM Authors WITH(NOLOCK) WHERE AuthorId = @AuthorId", new { AuthorId = id });
                if (auhtor != null)
                {
                    return true;
                    _logger.LogInformation("Author exists");
                }
                else return false;

            }

            _logger.LogError("Author does not exist");
            return false;

        }
    }
}
