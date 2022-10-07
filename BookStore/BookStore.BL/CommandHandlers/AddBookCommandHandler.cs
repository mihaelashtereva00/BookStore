using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddBookCommandHandler> _logger;

        public AddBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<AddBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<BookResponse> IRequestHandler<AddBookCommand, BookResponse>.Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _bookRepository.GetById(request.bookRequest.Id);

                if (book != null)
                {
                    return new BookResponse()
                    {
                        Book = book,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Book already exist"
                    };
                }

                if (request.bookRequest.Id <= 0)
                {
                    return new BookResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = $"Parameter id:{request.bookRequest.Id} must be greater than 0"
                    };
                }
                var b = _mapper.Map<Book>(request.bookRequest);
                var result = _bookRepository.AddBook(b);

                return new BookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = await result,
                    Message = "Successfully added book"
                };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
