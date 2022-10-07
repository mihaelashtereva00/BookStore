using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.DeleteBook(request.bookId);
        }
    }
}
