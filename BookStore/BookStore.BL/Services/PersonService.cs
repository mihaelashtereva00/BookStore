using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonInMemoryRepository _personRepository;
        public PersonService(IPersonInMemoryRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public Person? AddUser(Person user)
        {
            return _personRepository.AddUser(user);
        }

        public Person? DeleteUser(int userId)
        {
            return _personRepository.DeleteUser(userId);
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _personRepository.GetAllUsers();
        }

        public Person? GetById(int id)
        {
           return _personRepository.GetById(id);
        }

        public Person UpdateUser(Person user)
        {
           return _personRepository.UpdateUser(user);
        }
    }
}