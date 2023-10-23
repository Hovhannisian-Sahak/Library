using Azure.Core;
using Core.Domains;
using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> StudentSignup(StudentSignupModel request)
        {
            return await _studentRepository.StudentSignup(request);
        }

        public async Task<Student> StudentLogin(StudentLoginModel request)
        {
            return await _studentRepository.StudentLogin(request);
        }
        public async Task<object> BorrowBook(BorrowBookModel request)
        {
            return await _studentRepository.BorrowBook( request);
        }
        public async Task<object> DeliverBook(DeliverBookModel request)
        {
            return await _studentRepository.DeliverBook(request);
        }
        public async Task<List<Book>> FilterBooksByNameAndAuthor(FilteredBooks request)
        {
           return await _studentRepository.FilterBooksByNameAndAuthor(request);
        }
    }
}
