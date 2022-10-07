using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record AddBookCommand(BookRequest bookRequest) : IRequest<BookResponse>
    {
    }
}
