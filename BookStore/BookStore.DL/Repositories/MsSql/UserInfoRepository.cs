using BookStore.DL.Interfaces;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSql
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ILogger<UserInfoRepository> _logger;
        private readonly IConfiguration _configuration;
        public UserInfoRepository(ILogger<UserInfoRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<UserInfo> CreateeUserInfo(UserInfo userInfo)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "CREATE UserInfo SET UserId = @UserId, DisplayName = @DisplayName, UserName = @UserName, Email = @Email, Password = @Password, CreatedDate = @CreatedDate";
                    var result = await conn.ExecuteScalarAsync(query, userInfo);
                    return userInfo;

                }
            }
            catch (Exception )
            {
                _logger.LogError($"Coud not create user info");
            }
            return null;
        }

        public async Task<UserInfo?> DeleteUserInfo(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("DELETE FROM UserInfo WHERE Id = @Id", new { Id = userId });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not delete User Info");
                return null;
            }
        }



        public async Task<UserInfo> UpdateUserInfo(UserInfo userInfo)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE UserInfo SET UserId = @UserId, DisplayName = @DisplayName, UserName = @UserName, Email = @Email, Password = @Password, CreatedDate = @CreatedDate WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, userInfo);
                    return userInfo;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateUserInfo)} - {ex.Message}", ex);
            }
            return null;
        }

        
        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email = @Email AND Password = @Password", new { Email = email, Password = password });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not find UserInfo");
            }
            return new UserInfo();
        }
    }
}
