using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.MediatR.Commands
{
    public record class GetByIdCommand(int bookId) : IRequest<Book>
    {
    }
}
