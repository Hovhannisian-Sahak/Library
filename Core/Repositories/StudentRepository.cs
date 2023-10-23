using Azure.Core;
using Core.Domains;
using Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryManagementDbContext _db;
        //private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentRepository(LibraryManagementDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Student> StudentSignup(StudentSignupModel request)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var newStudent = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                FacultyId = request.FacultyId,
                AcademicYear = request.AcademicYear,
                Email = request.Email
            };
            _db.Students.Add(newStudent);
            _db.SaveChanges();
            var newPassword = new StudentPassword
            {
                StudentId = newStudent.Id,
                HashedPassword = hashedPassword
            };
            _db.StudentPasswords.Add(newPassword);
            _db.SaveChanges();
            return newStudent;
        }
        public async Task<Student> StudentLogin(StudentLoginModel request)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s=>s.Email==request.Email);
            if(student==null)
            {
                return null;
            }
            var password = await _db.StudentPasswords.FirstOrDefaultAsync(s => s.StudentId == student.Id);
            if(password == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, password.HashedPassword))
            {
                return null;
            }
            return student;

        }


        bool IsBorrowed(int studentId, int bookId)
        {
            var borrowed = _db.Histories.FirstOrDefault(h => h.BookId == bookId && h.StudentId == studentId && h.ReturnDate == null);
            if (borrowed != null)
            {
                return true;
            }
            return false;
        }

        bool HasCount(int bookId)
        {
            var bookCount = _db.Books.FirstOrDefault(book => book.Id == bookId && book.Count > 0);
            if (bookCount != null)
            {
                return true;
            }
            return false;
        }
        public int? GetBearerTokenFromCookie()
        {
            string tokenCookie = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenCookie);
            if (token == null) { return null; }
            var idClaim = token.Claims.FirstOrDefault(c => c.Type == "id");
            if (idClaim != null && int.TryParse(idClaim.Value, out int idValue))
            {
                return idValue;
            }
            return null;
        }
        public async Task<object> BorrowBook(BorrowBookModel request)
        {
            var studentId = GetBearerTokenFromCookie();
            var borrowed = IsBorrowed((int)studentId, request.BookId);
            if (borrowed)
            {
                return new FailToBorrowResult { IsSuccess = false,Message="student can not borrow the book because currently with the student yet"};
            }
            var count = HasCount(request.BookId);
            if (!count)
            {
                return new FailToBorrowResult { IsSuccess = false, Message = "the book is over" };
            }
            if (!request.date.HasValue)
            {
                request.date = null;
            }
            
            var parameters = new[] {new SqlParameter("@StudentId", studentId),
                    new SqlParameter("@BookId", request.BookId),
                    new SqlParameter("@EmployeeId", request.EmployeeId),
                    new SqlParameter("@Date", request.date ?? (object)DBNull.Value)};

             await _db.Database.ExecuteSqlRawAsync("EXEC BorrowBook @StudentId, @BookId, @EmployeeId, @Date",parameters);
            await _db.SaveChangesAsync();
            var borrowedBook = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.BookId);
            return borrowedBook;
        }
        public async Task<object> DeliverBook(DeliverBookModel request)
        {
            var borrowed = IsBorrowed(request.StudentId, request.BookId);
            if (!borrowed)
            {
                return new FailToBorrowResult { IsSuccess = false, Message = "the student can not deliver this book,because currently not with the student" };
            }
            var parameters = new[]
            {
                new SqlParameter("@StudentId", request.StudentId),
                    new SqlParameter("@BookId", request.BookId)
            };
            await _db.Database.ExecuteSqlRawAsync($"EXEC DeliverBook @StudentId,@BookId",parameters);
            await _db.SaveChangesAsync();
            var deliveredBook = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.BookId);
            return deliveredBook;
        }

        public async Task<List<Book>> FilterBooksByNameAndAuthor(FilteredBooks request)
        {
            var books = _db.Books.AsQueryable();
            if (books == null) { return null; }
            List<Book> filteredBooks = new();
            if (!string.IsNullOrEmpty(request.Name))
            {
                books = books.Where(book => book.Name.Contains(request.Name));
                filteredBooks.AddRange(books);
            }
            if (!string.IsNullOrEmpty(request.Author))
            {
                books = books.Where(book => book.Author.StartsWith(request.Author));
                filteredBooks.AddRange(books);
            }

            if (filteredBooks.Count == 0) { return null; }
            return filteredBooks;
        }


    }
}
