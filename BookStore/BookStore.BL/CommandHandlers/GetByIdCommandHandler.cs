using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.CommandHandlers
{
    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
             return await _bookRepository.GetById(request.bookId);
        }

    }
}
