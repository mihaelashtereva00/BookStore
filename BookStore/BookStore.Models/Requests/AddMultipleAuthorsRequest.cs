using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Requests
{
    public class AddMultipleAuthorsRequest
    {
        public IEnumerable<AuthorRequest> AuthorRequests { get; set; }
        public string Reason { get; set; }
    }
}
