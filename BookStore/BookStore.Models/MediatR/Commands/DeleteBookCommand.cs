using BookStore.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.MediatR.Commands
{
    public record DeleteBookCommand(int bookId) : IRequest<Book>
    {
    }
}
