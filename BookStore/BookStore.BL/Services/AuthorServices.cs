using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly IAuthorInMemoryRepository _authorInMemoryRepository;
        public AuthorServices(IAuthorInMemoryRepository authorInMemoryRepository)
        {
            _authorInMemoryRepository = authorInMemoryRepository;
        }

        public Author? AddAuthor(Author author)
        {
           return _authorInMemoryRepository.AddAuthor(author);
        }

        public Author? DeleteAuthor(int userId)
        {
            return _authorInMemoryRepository.DeleteAuthor(userId);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorInMemoryRepository.GetAllAuthors();
        }

        public Author? GetById(int id)
        {
            return _authorInMemoryRepository.GetById(id);
        }

        public Author? GetByNickname(string nickname)
        {
            return _authorInMemoryRepository.GetByNickname(nickname);
        }

        public Author UpdateAuthor(Author user)
        {
            return _authorInMemoryRepository.UpdateAuthor(user);
        }
    }
}
