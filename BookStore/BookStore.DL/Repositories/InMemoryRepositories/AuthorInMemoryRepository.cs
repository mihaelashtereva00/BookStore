using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorInMemoryRepository
    {
        private static List<Author> _authors = new List<Author>()
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

        public Author? AddAuthor(Author author)
        {
            try
            {
                _authors.Add(author);
            }
            catch (Exception e)
            {
                return null;
            }

            return author;
        }

        public Author? DeleteAuthor(int authorId)
        {
            if (authorId <= 0) return null;

            var auth = _authors.FirstOrDefault(x => x.Id == authorId);

            _authors.Remove(auth);

            return auth;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }

        public Author? GetById(int id)
        {
            return _authors.FirstOrDefault(a => a.Id == id);
        }

        public Author? GetByNickname(string nickname)
        {
            return _authors.FirstOrDefault(a => a.Nickname == nickname);
        }

        public Author UpdateAuthor(Author author)
        {
            var existingAuthorr = _authors.FirstOrDefault(x => x.Id == author.Id);

            if (existingAuthorr == null) return null;

            _authors.Remove(existingAuthorr);

            _authors.Add(author);

            return author;
        }


    }
}
