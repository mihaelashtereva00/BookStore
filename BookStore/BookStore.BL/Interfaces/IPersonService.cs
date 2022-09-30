using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAllUsers();

        public Person? GetById(int id);

        public Person? AddUser(Person user);

        public Person UpdateUser(Person user);

        public Person? DeleteUser(int userId);
    }
}
