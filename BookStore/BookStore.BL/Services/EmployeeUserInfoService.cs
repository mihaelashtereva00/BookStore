using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.User;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class EmployeeUserInfoService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeUserInfoService> _logger;

        public EmployeeUserInfoService(IEmployeesRepository employeesRepository, IMapper mapper, ILogger<EmployeeUserInfoService> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _employeesRepository = employeesRepository;
        }
        public async Task AddEmployee(Employee employee)
        {
             await  _employeesRepository.AddEmployee(employee);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeesRepository.CheckEmployee(id);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeesRepository.DeleteEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeesRepository.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeesRepository.GetEmployeeDetails(id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeesRepository.UpdateEmployee(employee);
        }
    }
}
