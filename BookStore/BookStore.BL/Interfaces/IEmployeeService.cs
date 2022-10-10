using BookStore.Models.Models.User;

namespace BookStore.BL.Interfaces
{
    public interface IEmployeeService
    {
        public Task AddEmployee(Employee employee);
        public Task<bool> CheckEmployee(int id);
        public Task DeleteEmployee(int id);
        public Task<IEnumerable<Employee>> GetEmployeeDetails();
        public Task<Employee?> GetEmployeeDetails(int id);
        public Task UpdateEmployee(Employee employee);
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);

    }
}
