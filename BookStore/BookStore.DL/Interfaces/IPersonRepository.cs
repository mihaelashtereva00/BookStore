using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllUsers();
        Person? GetById(int id);
        Person? AddUser(Person user);
        Person UpdateUser(Person user);
        Person? DeleteUser(int userId);
    }
}
