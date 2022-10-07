using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
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
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddAuthorCommand> _logger;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, ILogger<AddAuthorCommand> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<AuthorResponse> IRequestHandler<AddAuthorCommand, AuthorResponse>.Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _authorRepository.GetByName(request.authorRequest.Name);

                if (auth != null)
                    return new AuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(request.authorRequest);
                var result = await _authorRepository.AddAuthor(author);

                return new AuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result,
                    Message = "Successfully added"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not add author");
                throw new Exception(e.Message);
            }
        }
    }
}
