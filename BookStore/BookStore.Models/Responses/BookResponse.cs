using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Responses
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; set; }

    }
}
