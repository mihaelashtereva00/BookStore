using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models.User
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }
    }
}
