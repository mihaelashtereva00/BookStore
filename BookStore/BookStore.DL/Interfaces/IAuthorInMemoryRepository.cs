using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorInMemoryRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author? GetByNickname(string nickname);
        Author? AddAuthor(Author author);
        Author UpdateAuthor(Author user);
        Author? DeleteAuthor(int userId);
    }
}
