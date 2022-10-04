using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author?> GetById(int id);
        Task<Author?> GetByNickname(string nickname);
        Task<Author?> GetByName(string name);
        Task<AuthorResponse> AddAuthor(AuthorRequest author);
        Task<AuthorResponse> UpdateAuthor(AuthorRequest author);
        Task<Author?> DeleteAuthor(int userId);
        Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
