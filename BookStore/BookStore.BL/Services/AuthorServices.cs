using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace BookStore.BL.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly IAuthorRepository _authorInMemoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorServices> _logger;

        public AuthorServices(IAuthorRepository authorInMemoryRepository, IMapper mapper, ILogger<AuthorServices> logger)
        {
            _authorInMemoryRepository = authorInMemoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthorResponse> AddAuthor(AuthorRequest authorRequest)
        {
            try
            {
                var auth = await _authorInMemoryRepository.GetByName(authorRequest.Name);

                if (auth != null)
                return new AuthorResponse()
                {
                    Author = auth,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author already exist"
                };

                var author = _mapper.Map<Author>(authorRequest);
                var result = await _authorInMemoryRepository.AddAuthor(author);

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

        public async Task<Author?> DeleteAuthor(int userId)
        {
            _logger.LogInformation("Successfully deleted author");
            return await _authorInMemoryRepository.DeleteAuthor(userId);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorInMemoryRepository.GetAllAuthors();
        }

        public async Task<Author?> GetById(int id)
        {
            _logger.LogInformation("Successfully got author by ID");
            return await _authorInMemoryRepository.GetById(id);
        }

        public async Task<Author?> GetByNickname(string nickname)
        {
            _logger.LogInformation("Successfully got author by nickname");
            return await _authorInMemoryRepository.GetByNickname(nickname);
        }
        public async Task<Author?> GetByName(string name)
        {
            _logger.LogInformation("Successfully got author by name");
            return await _authorInMemoryRepository.GetByName(name);
        }

        public async Task<AuthorResponse> UpdateAuthor(AuthorRequest authorRequest)
        {
            try
            {

                var auth = await _authorInMemoryRepository.GetById(authorRequest.Id);
               

                if (auth == null)
                {
                    _logger.LogError("Author does not exist - null");
                    return new AuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author does not exist - null"
                    };
                }

                var author = _mapper.Map<Author>(authorRequest);
                var result = await _authorInMemoryRepository.UpdateAuthor(author);

                _logger.LogInformation("Successfully updated author");
                return new AuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result,
                    Message = "Successfully updated"
                };
            }
            catch (Exception e)
            {

                _logger.LogError("Could not update author");
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return await _authorInMemoryRepository.AddMultipleAuthors(authorCollection);
        }


    }
}
