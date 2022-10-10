using BookStore.DL.Interfaces;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSql
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserInfoStore(IEmployeesRepository employeeRepository, IConfiguration configuration, IPasswordHasher<UserInfo> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = @"INSERT INTO UserInfo (DisplayName, UserName, Email, Password, CreatedDate) 
                                  VALUES ( @DisplayName, @UserName, @Email, @Password, @CreatedDate)";

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = await conn.ExecuteAsync(query, user);

                    return IdentityResult.Success;
                }
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = $"Error in UserInfoStore: {e.Message}"
                });
            }
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("Select * FROM UserInfo WHERE UserName = @UserName", new { UserName = userName }); ;
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);

        }

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WHERE UserId = @UserId", new { user.UserId });
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var result =
                        await conn.QueryAsync<string>("SELECT r.RoleName FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId )", new { UserId = user.UserId });

                    return result.ToList();
                }
                catch (Exception e)
                {
                    //_logger.LogError($"Error in {nameof(UserRoleStore.FindByNameAsync)}:{e.Message}");
                    return null;
                }
            }
        }

        public async Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync(cancellationToken);
                var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("Select * FROM UserInfo WHERE UserId = @UserId", new { UserId = user.UserId }); ;
                return result?.UserId.ToString();
            }
        }

        public Task<string?> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);
            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordhash WHERE UserId = @userId", new { userId = user.UserId, passwordHash });

        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
