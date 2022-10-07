using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddBookCommandHandler> _logger;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<AddBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }
        async Task<BookResponse> IRequestHandler<UpdateBookCommand, BookResponse>.Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _bookRepository.GetById(request.bookRequest.Id);

                if (book == null)
                {
                    return new BookResponse()
                    {
                        Book = book,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Book does not exist"
                    };
                }

                var b = _mapper.Map<Book>(request.bookRequest);
                var result = await _bookRepository.UpdateBook(b);

                return new BookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result,
                    Message = "Successfully updated book"
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error when Updating book with Id {request.bookRequest.Id} : {e}");
                throw new Exception(e.Message); ;
            }
        }
    }
}
