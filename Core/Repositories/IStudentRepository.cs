using Core.Domains;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> StudentSignup(StudentSignupModel request);
        Task<Student> StudentLogin(StudentLoginModel request);
        Task<object> BorrowBook(BorrowBookModel request);
        Task<object> DeliverBook(DeliverBookModel request);
        Task<List<Book>> FilterBooksByNameAndAuthor(FilteredBooks request);
    }
}
