using Azure.Core;
using Core.Domains;
using Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Core.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LibraryManagementDbContext _db;
        public EmployeeRepository(LibraryManagementDbContext db)
        {
            _db = db;
        }
        public async Task<object> AddEmployee(AddEmployeeModel request,string hashedPassword)
        {
            var existing=_db.Employees.FirstOrDefault(e => e.Email == request.Email);
            if (existing != null) { return null; }
            var newEmployee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                ProfessionId = request.ProfessionId,
                HireDate = DateTime.UtcNow,
                Salary = request.Salary,
                Email=request.Email
            };
             await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
            var newPassword = new EmployeePassword
            {
                EmployeeId = newEmployee.Id,
                HashedPassword = hashedPassword
            };
            await _db.EmployeePasswords.AddAsync(newPassword);
            await _db.SaveChangesAsync();
            return newEmployee;
        }
        public async Task<Employee> EmployeeLogin(EmployeeLoginModel request)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (employee == null)
            { 
                return null; 
            }
            var password = await _db.EmployeePasswords.FirstOrDefaultAsync(p => p.EmployeeId == employee.Id);
            if (password == null)
            { 
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, password.HashedPassword))
            {
                return null;
            }
            return employee;
        }
        public async Task<Employee> EditEmployee(EditEmployeeModel request)
        {
            var parameters = new[]
            {
        new SqlParameter("@EmployeeId", request.Id),
        new SqlParameter("@FirstName", request.FirstName ?? (object)DBNull.Value),
        new SqlParameter("@LastName", request.LastName ?? (object)DBNull.Value),
        new SqlParameter("@Salary", request.Salary ?? (object)DBNull.Value),
         new SqlParameter("@ProfessionId", request.ProfessionId ?? (object)DBNull.Value)
    };

            await _db.Database.ExecuteSqlRawAsync("EXEC EditEmployee @EmployeeId, @FirstName, @LastName, @Salary,@ProfessionId", parameters);

            var updatedemployee = _db.Employees.FirstOrDefault(b => b.Id == request.Id);

            return updatedemployee;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            var employeeToDelete = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employeeToDelete == null) { return null; }
            _db.Employees.Remove(employeeToDelete);
            await _db.SaveChangesAsync();
            return employeeToDelete;
        }
        public async Task<Book> AddBook(AddBookModel request)
        {
                var newBook = new Book
                {
                    Name = request.Name,
                    Author = request.Author,
                    PublishDate = request.PublishDate,
                    Count = request.Count
                };

                _db.Books.Add(newBook);

                await _db.SaveChangesAsync();

                return newBook;
            
        }
        public async Task<Book> EditBook(EditBookModel request)
        {
            var parameters = new[]
            {
        new SqlParameter("@BookID", request.Id),
        new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
        new SqlParameter("@Author", request.Author ?? (object)DBNull.Value),
        new SqlParameter("@BookCount", request.Count ?? (object)DBNull.Value)
    };

            await _db.Database.ExecuteSqlRawAsync("EXEC EditBook @BookID, @Name, @Author, @BookCount", parameters);

            var updatedBook = _db.Books.FirstOrDefault(b => b.Id == request.Id);

            return updatedBook;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var existingBook = await _db.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (existingBook == null) { return null; }
            _db.Books.Remove(existingBook);
            await _db.SaveChangesAsync();
            return existingBook;
        }
        public async Task<IEnumerable<EmployeesWithBooks>> EmployeesWithBooks()
        {
            var result = await _db.EmployeesWithBooksResult
                .FromSqlRaw("EXEC GetEmployeesWithBooks")
                .ToListAsync();
            if (result == null) { return null; }
            //foreach(var res in result)
            //{
            //    if (res.OfferedBooks == null)
            //    {
            //        result.Remove(res);
            //    }
            //}
            return result;
        }
        public async Task<IEnumerable<StudentsMostReadBooks>> StudentsMostReadBooks()
        {
            var result = await _db.StudentsMostReadBooksResult
                .FromSqlRaw("EXEC StudentsMostReadBooks")
                .ToListAsync();
            if (result == null) { return null; }
            return result;
        }
        public async Task<IEnumerable<StudentsWithLateReturn>> StudentsWithLateReturn()
        {
            
            var result = await _db.StudentsWithLateReturnResult
                .FromSqlRaw("EXEC StudentsWithLateReturns")
                .ToListAsync();
            if (result == null) { return null; }
            return result;
        }
        public async Task<object> PromoteToAdmin(int id)
        {
            var existing = await _db.Employees.FirstOrDefaultAsync(e =>e.Id==id);
            
            if(existing!=null) { 
                if(existing.RoleId==1)
                {
                    return "the employee with this id is already have role of admin";
                }
                else
                {
                    existing.RoleId = 1;
                    _db.Employees.Update(existing); 
                    await _db.SaveChangesAsync(); 
                    return existing;
                }
            }
            return existing;
           
        }

        public async Task<List<FilteredBooks>> FilterBooks(FilterBooksModel request)
        {
            var parameters = new[]
            {
             new SqlParameter("@SearchText", request.SearchText ?? ""),
            };

            var filteredBooks = await _db.FilteredBooksResult.FromSqlRaw("EXEC FilterBooks @SearchText", parameters).ToListAsync();
            if (filteredBooks.Count==0) { return null; }
            return filteredBooks;
        }
        public async Task<List<FilteredEmployees>> FilterEmployees(FilterEmployeesModel request)
        {
            var parameters = new[]
            {
             new SqlParameter("@SearchText", request.SearchText ?? ""),
             new SqlParameter("@ProfessionId", request.ProfessionId ?? (object)DBNull.Value),
             new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
             new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
            };

            var filteredEmployees = await _db.FilteredEmployeesResult.FromSqlRaw("EXEC FilterEmployees @SearchText,@ProfessionId,@StartDate,@EndDate", parameters).ToListAsync();
            if (filteredEmployees.Count == 0) { return null; }
            return filteredEmployees;
        }
    }
}
