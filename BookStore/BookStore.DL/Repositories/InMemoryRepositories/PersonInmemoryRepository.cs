using BookStore.DL.Interfaces;
using BookStore.Models;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class PersonInmemoryRepository : IPersonInMemoryRepository
    {
        private static List<Person> _users = new List<Person>()
        {
        new Person()
        {
            Id = 1,
            Name = "Pesho",
            Age = 20
        },
        new Person()
        {
            Id = 2,
            Name = "Kerana",
            Age = 23
        } };

        //public Guid Id { get; set; }

        public PersonInmemoryRepository()
        {
           // Id = Guid.NewGuid();
        }

        public IEnumerable<Person> GetAllUsers() => _users;

        public Person? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddUser(Person user)
        {
            try
            {
                _users.Add(user);
            }
            catch (Exception e)
            {
                return null;
            }

            return user;
        }

        public Person UpdateUser(Person user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);

            if (existingUser == null) return null;

            _users.Remove(existingUser);

            _users.Add(user);

            return user;
        }

        public Person? DeleteUser(int userId)
        {
            if (userId <= 0) return null;

            var user = _users.FirstOrDefault(x => x.Id == userId);

            _users.Remove(user);

            return user;
        }
        //public Guid GetGuidId()
        //{
        //    return Id;
        //}
    }
}