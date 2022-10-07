using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MsSql;
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
    public class UpdateAuthorCommandHandler: IRequestHandler<UpdateAuthorCommand ,AuthorResponse>
    {
        private readonly IAuthorService _authorReposiory;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;

        public UpdateAuthorCommandHandler(IAuthorService authorRepository, IMapper mapper, ILogger<UpdateAuthorCommandHandler> logger)
        {
            _authorReposiory = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }
        async Task<AuthorResponse> IRequestHandler<UpdateAuthorCommand, AuthorResponse>.Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorReposiory.UpdateAuthor(request.authorRequest);

            try
            {

                var auth = await _authorReposiory.GetById(request.authorRequest.Id);


                if (auth == null)
                {
                    _logger.LogError("Author does not exist - null");
                    return new AuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author does not exist"
                    };
                }

                var result = await _authorReposiory.UpdateAuthor(request.authorRequest);

                _logger.LogInformation("Successfully updated author");
                return new AuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result.Author,
                    Message = "Successfully updated"
                };
            }
            catch (Exception e)
            {

                _logger.LogError("Could not update author");
                throw new Exception(e.Message);
            }
        }
    }
}
