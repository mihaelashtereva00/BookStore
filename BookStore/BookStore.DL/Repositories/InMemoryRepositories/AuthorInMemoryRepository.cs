using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorRepository
    {
        private readonly ILogger _logger;

        private static List<Author> _authors = new()
        {
        new Author()
        {
            Id = 1,
            Name = "Petya",
            Age = 31
        },
        new Author()
        {
            Id = 2,
            Name = "Katya",
            Age = 19
        } };

        public Author AddAuthor(Author author)
        {
            try
            {
                _authors.Add(author);
                _logger.LogInformation("Author was added");
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not add author");
                return null;
            }

            return author;
        }

        public Author? DeleteAuthor(int authorId)
        {
            if (authorId <= 0)
                _logger.LogError("The ID of the author is not valid");
                return null;

            var auth = _authors.FirstOrDefault(x => x.Id == authorId);

            _authors.Remove(auth);
            _logger.LogInformation("Successfully removed author");

            return auth;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            _logger.LogInformation("Successfully got all authors");
            return _authors;
        }

        public Author? GetById(int id)
        {
            _logger.LogInformation("Successfully got author by ID");
            return _authors.FirstOrDefault(a => a.Id == id);
        }

        public Author? GetByNickname(string nickname)
        {
            _logger.LogInformation("Successfully got author by Nickname");
            return _authors.FirstOrDefault(a => a.Nickname == nickname);
        }
        public Author? GetByName(string name)
        {
            _logger.LogInformation("Successfully got author by Name");
            return _authors.FirstOrDefault(a => a.Name == name);
        }

        public Author UpdateAuthor(Author author)
        {
            try
            {
                var existingAuthorr = _authors.FirstOrDefault(x => x.Id == author.Id);

                if (existingAuthorr == null) return null;

                _authors.Remove(existingAuthorr);

                _authors.Add(author);

                _logger.LogInformation("Successfully updated author");
                return author;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Could not update user");
                throw;
            }
        }


    }
}
