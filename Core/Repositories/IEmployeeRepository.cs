using Core.Domains;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<object> AddEmployee(AddEmployeeModel request, string hashedPassword);
        Task<Employee> EmployeeLogin(EmployeeLoginModel request);
        Task<Employee> EditEmployee(EditEmployeeModel request);
        Task<Book> AddBook(AddBookModel request);
        Task<Book> EditBook(EditBookModel request);
        Task<Book> DeleteBook(int id);
        Task<IEnumerable<EmployeesWithBooks>> EmployeesWithBooks();
        Task<IEnumerable<StudentsWithLateReturn>> StudentsWithLateReturn();
        Task<IEnumerable<StudentsMostReadBooks>> StudentsMostReadBooks();
        Task<List<FilteredBooks>> FilterBooks(FilterBooksModel request);
        Task<List<FilteredEmployees>> FilterEmployees(FilterEmployeesModel request);
        Task<Employee> DeleteEmployee(int id);
        Task<object> PromoteToAdmin(int id);

    }
}
