using BookStore.Models.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);
        Task<UserInfo?> CheckUserAndPass(string userName, string password);
        public Task<IEnumerable<string>> GetUserRoles(UserInfo user);
    }
}
