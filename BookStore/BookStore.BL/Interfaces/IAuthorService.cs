using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author? GetByNickname(string nickname);
        Author? GetByName(string name);
        AuthorResponse AddAuthor(AuthorRequest author);
        AuthorResponse UpdateAuthor(AuthorRequest author);
        Author? DeleteAuthor(int userId);
    }
}
