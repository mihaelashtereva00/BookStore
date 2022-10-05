using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author?> GetById(int id);
        Task<Author?> GetByNickname(string nickname);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author user);
        Task<Author?> DeleteAuthor(int userId);
        Task<Author?> GetByName(string name);
        Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
