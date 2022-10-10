using BookStore.Models.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DL.Interfaces
{
    public interface IUserInfoRepository
    {

        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
