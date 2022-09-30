using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author? GetByNickname(string nickname);
        Author? AddAuthor(Author author);
        Author UpdateAuthor(Author user);
        Author? DeleteAuthor(int userId);
    }
}
