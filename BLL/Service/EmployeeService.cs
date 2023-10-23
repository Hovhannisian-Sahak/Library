using Azure.Core;
using Core.Domains;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BLL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        public async Task<object> AddEmployee(AddEmployeeModel request,string hashedPassword)

        {
            hashedPassword = HashPassword(request.Password);
            return await _employeeRepository.AddEmployee(request, hashedPassword);
        }
        public async Task<Employee> EmployeeLogin(EmployeeLoginModel request)
        {
            return await _employeeRepository.EmployeeLogin(request);
        }
        public async Task<Employee> EditEmployee(EditEmployeeModel request)
        {
            return await _employeeRepository.EditEmployee(request);
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }
        public async Task<Book> AddBook(AddBookModel request)
        {
            return await _employeeRepository.AddBook(request);
        }
        public async Task<Book> EditBook(EditBookModel request)
        {
            return await _employeeRepository.EditBook(request);
        }
        public async Task<Book> DeleteBook(int id)
        {
            return await _employeeRepository.DeleteBook(id);
        }
        public async Task<IEnumerable<StudentsMostReadBooks>> StudentsMostReadBooks()
        {

            return await _employeeRepository.StudentsMostReadBooks();
        }
        public async Task<IEnumerable<StudentsWithLateReturn>> StudentsWithLateReturn()
        {
            return await _employeeRepository.StudentsWithLateReturn();
        }
        public async Task<IEnumerable<EmployeesWithBooks>> EmployeesWithBooks()
        {
           return await _employeeRepository.EmployeesWithBooks();
        }
        public async Task<object> PromoteToAdmin(int id)
        {
            return await _employeeRepository.PromoteToAdmin(id);
        }

        public async Task<List<FilteredEmployees>> FilterEmployees(FilterEmployeesModel request)
        {
            return await _employeeRepository.FilterEmployees(request);
        }

        public async Task<List<FilteredBooks>> FilterBooks(FilterBooksModel request)
        {
            return await _employeeRepository.FilterBooks(request);
        }
    }
}
