using Azure.Core;
using Core.Domains;
using Core.Models;

namespace BLL.Service
{
    public interface IStudentService
    {
        Task<Student> StudentSignup(StudentSignupModel request);
        Task<Student> StudentLogin(StudentLoginModel request);
        Task<object> BorrowBook(BorrowBookModel request);
        Task<object> DeliverBook(DeliverBookModel request);
        Task<List<Book>> FilterBooksByNameAndAuthor(FilteredBooks request);
    }
}
