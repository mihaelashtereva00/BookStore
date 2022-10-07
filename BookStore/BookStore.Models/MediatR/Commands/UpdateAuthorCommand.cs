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
    public record UpdateAuthorCommand(AuthorRequest authorRequest) : IRequest<AuthorResponse>
    {
    }
}
