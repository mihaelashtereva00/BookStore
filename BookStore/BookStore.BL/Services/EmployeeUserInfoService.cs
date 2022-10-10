using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class EmployeeUserInfoService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeUserInfoService> _logger;

        public EmployeeUserInfoService(IEmployeesRepository employeesRepository, IUserInfoRepository userInfoRepository, IMapper mapper, ILogger<EmployeeUserInfoService> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _employeesRepository = employeesRepository;
            _userInfoRepository = userInfoRepository;
        }
        public Task AddEmployee(Employee employee)
        {
            return _employeesRepository.AddEmployee(employee);
        }

        public Task<bool> CheckEmployee(int id)
        {
            return _employeesRepository.CheckEmployee(id);
        }

        public Task DeleteEmployee(int id)
        {
            return _employeesRepository.DeleteEmployee(id);
        }

        public Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return _employeesRepository.GetEmployeeDetails();
        }

        public Task<Employee?> GetEmployeeDetails(int id)
        {
            return _employeesRepository.GetEmployeeDetails(id);
        }

        public Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            return _userInfoRepository.GetUserInfoAsync(email, password);
        }

        public Task UpdateEmployee(Employee employee)
        {
            return _employeesRepository.UpdateEmployee(employee);
        }
    }
}
