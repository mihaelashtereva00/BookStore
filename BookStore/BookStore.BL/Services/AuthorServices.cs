using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly IAuthorRepository _authorInMemoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthorServices(IAuthorRepository authorInMemoryRepository, IMapper mapper)
        {
            _authorInMemoryRepository = authorInMemoryRepository;
            _mapper = mapper;
        }

        public AuthorResponse AddAuthor(AuthorRequest authorRequest)
        {
            try
            {
                var auth = _authorInMemoryRepository.GetByName(authorRequest.Name);

                if (auth != null)
                    _logger.LogInformation("Author already exist");
                    return new AuthorResponse()
                    {
                    Author = auth,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(authorRequest);
                var result = _authorInMemoryRepository.AddAuthor(author);

                _logger.LogInformation("Successfully added author");
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

        public Author? DeleteAuthor(int userId)
        {
            _logger.LogInformation("Successfully deleted author");
            return _authorInMemoryRepository.DeleteAuthor(userId);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            _logger.LogInformation("Successfully got all authors");
            return _authorInMemoryRepository.GetAllAuthors();
        }

        public Author? GetById(int id)
        {
            _logger.LogInformation("Successfully got author by ID");
            return _authorInMemoryRepository.GetById(id);
        }

        public Author? GetByNickname(string nickname)
        {
            _logger.LogInformation("Successfully got author by nickname");
            return _authorInMemoryRepository.GetByNickname(nickname);
        }
        public Author? GetByName(string name)
        {
            _logger.LogInformation("Successfully got author by name");
            return _authorInMemoryRepository.GetByName(name);
        }

        public AuthorResponse UpdateAuthor(AuthorRequest authorRequest)
        {
            try
            {

                var auth = _authorInMemoryRepository.GetByName(authorRequest.Name);

                if (auth == null)
                    _logger.LogError("Author does not exist");
                return new AuthorResponse()
                {
                    Author = auth,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author does not exist"
                };

                var author = _mapper.Map<Author>(auth);
                var result = _authorInMemoryRepository.UpdateAuthor(author);

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
    }
}
