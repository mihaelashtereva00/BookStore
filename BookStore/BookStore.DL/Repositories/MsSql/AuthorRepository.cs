using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSql
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;
        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES ( @Name, @Age,  @DateOfBirth,  @NickName)";
                    var resul = conn.ExecuteAsync(query, author);
                    return author;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add author");
                return null;
            }
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync("INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES (@Name, @Age,  @DateOfBirth,  @NickName)"
                        , authorCollection);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not add author :{e}");
                return false;
            }
        }

        public async Task<Author?> DeleteAuthor(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                        await conn.OpenAsync();
                        return await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Authors WHERE Id = @Id", new { Id = userId });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not add author");
                return null;
            }
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Author>("SELECT * FROM Authors");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}:{e.Message}", e);
            }
            return Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author?> GetByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Name = @Name", new { Name = name });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetByName)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author?> GetByNickname(string nickname)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Nickname = @Nickname", new { Nickname = nickname });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetByNickname)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author> UpdateAuthor(Author user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE Authors SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, NickName = @Nickname WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, user);
                    return user;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateAuthor)} - {ex.Message}", ex);
            }
            return null;
        }

        public async Task<bool> AuthorHasBooks(int authorId)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var books = await conn.QueryMultipleAsync("SELECT * FROM Books WITH(NOLOCK) WHERE AuthorId = @Id", new { Id = authorId });
                if (books != null) return true;
              
            }
            return false;
        }


    }
}
