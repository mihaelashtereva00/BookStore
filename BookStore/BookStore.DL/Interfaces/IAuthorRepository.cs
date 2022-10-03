using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author? GetByNickname(string nickname);
        Author AddAuthor(Author author);
        Author UpdateAuthor(Author user);
        Author? DeleteAuthor(int userId);
        Author? GetByName(string name);
    }
}
