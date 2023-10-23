using AutoMapper;
using BLL.Service;
using Core;
using Core.Domains;
using Core.Models;
using FluentValidation;
using Library.Models.RequestModels;
using Library.Models.ResponseModels;
using Library.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
       
        public EmployeeController(TokenService tokenService, IEmployeeService employeeService, IMapper mapper, LibraryManagementDbContext db)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        [HttpPost("/admin/addEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            var result = new Result();
            try
            {
                
                var validator = new AddEmployeeRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var EmployeeM = _mapper.Map<AddEmployeeModel>(request);

                var newEmployee = await _employeeService.AddEmployee(EmployeeM,request.Password);
                if (newEmployee == null)
                {
                    return NotFound();
                }
                if (newEmployee is FailToSignUp res)
                {
                    result.Message = res.Message;
                    result.isSuccess = res.isSuccess;
                    return BadRequest(result);
                }
                    result.Data = newEmployee;
                result.Message = "you successfully signed up";
                result.isSuccess = true;
                return StatusCode(StatusCodes.Status201Created, result);

            }
            catch (Exception)
            {
                result.isSuccess = false;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred on the server.");
            }
        }
        [HttpPost("login")]

        public async Task<ActionResult> EmployeeLogin([FromBody] EmployeeLoginRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new EmployeeLoginRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var EmployeeM = _mapper.Map<EmployeeLoginModel>(request);

                var loginEmployee = await _employeeService.EmployeeLogin(EmployeeM);
                if (loginEmployee == null)
                {
                    return NotFound();
                }
                string token="";
                if (loginEmployee.RoleId == 1)
                {
                     token = _tokenService.CreateToken(loginEmployee.Id, "Admin");
                }
                else if(loginEmployee.RoleId == 2)
                {
                    token =  _tokenService.CreateToken(loginEmployee.Id, "Employee");
                }
                HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.Now.AddHours(1)
                });

                result.Data = token;
                result.Message = "you successfully logged in";
                result.isSuccess = true;
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpPut("/admin/editEmployee")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> EditEmployee([FromBody] EditEmployeeRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new EditEmployeeRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var EditEmployeeM = _mapper.Map<EditEmployeeModel>(request);
                var employeeToEdit = await _employeeService.EditEmployee(EditEmployeeM);
                if (employeeToEdit == null) { return NotFound("Book not found."); }
                result.Message = "The employeeToEdit was edited succesfully";
                result.isSuccess = true;
                result.Data = employeeToEdit;
                return Ok(result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }
        [HttpDelete("/admin/deleteEmployee")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = new Result();

            try
            {
                var deletedEmployee = await _employeeService.DeleteEmployee(id);

                if (deletedEmployee == null)
                {
                    return NotFound("Employee not found.");
                }
                return NoContent(); 
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server.";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpPost("/admin/addBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> AddBook([FromBody] AddBookRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new AddBookRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var AddBookM = _mapper.Map<AddBookModel>(request);
                var newBook = await _employeeService.AddBook(AddBookM);
                if (newBook == null) { return NotFound("Book not found."); }
                result.Message = "The new book was created succesfully";
                result.isSuccess = true;
                result.Data = newBook;
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpPut("/admin/editBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> EditBook([FromBody] EditBookRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new EditBookRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var EditBookM = _mapper.Map<EditBookModel>(request);
                var bookToEdit = await _employeeService.EditBook(EditBookM);
                if (bookToEdit == null) { return NotFound("Book not found."); }
                result.Message = "The book was edited succesfully";
                result.isSuccess = true;
                result.Data = bookToEdit;
                return Ok(result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }
        [HttpDelete("/admin/deleteBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = new Result();
            try
            {
                var BookToDelete =await  _employeeService.DeleteBook(id);
                if (BookToDelete == null) { return NotFound("Book not found."); }
                return NoContent();
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpPost("filterBooks")]
        [Authorize]
        public async Task<ActionResult<List<Book>>> FilterBooks([FromBody] FilterBooksRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new FilterBooksRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var filteredBooksM = _mapper.Map<FilterBooksModel>(request);
                var booksTofilter= await _employeeService.FilterBooks(filteredBooksM);
                if (booksTofilter == null) { return NotFound("Books are not found."); }
                result.Data = booksTofilter;
                result.isSuccess = true;
                result.Message = "books filtered successfully";
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpPost("filterEmployees")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<Employee>>> FilterEmployees([FromBody] FilterEmployeesRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new FilterEmployeesRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var filteredEmployeesM = _mapper.Map<FilterEmployeesModel>(request);
                var employeeTofilter = await _employeeService.FilterEmployees(filteredEmployeesM);
                if (employeeTofilter == null) { return NotFound("Employees are not found."); }
                result.Data = employeeTofilter;
                result.isSuccess = true;
                result.Message = "employees filtered successfully";
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpGet("studentsMostReadBooks")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StudentsMostReadBooks>>> StudentsMostReadBooks()
        {
            var result = new Result();
            try
            {
                var studentsMostReadBooks = await _employeeService.StudentsMostReadBooks();
                if (studentsMostReadBooks == null) { return NotFound(); }
                result.Data = studentsMostReadBooks;
                result.isSuccess = true;
                result.Message = "students who have read books the most";
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpGet("studentsWithLateReturn")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StudentsWithLateReturn>>> StudentsWithLateReturn()
        {
            var result = new Result();
            try
            {
                var studentsWithLateReturn = await _employeeService.StudentsWithLateReturn();
                if (studentsWithLateReturn == null) { return NotFound(); }
                result.Data = studentsWithLateReturn;
                result.isSuccess = true;
                result.Message = "students who have given books late";
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpGet("/admin/employeesWithBooks")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<EmployeesWithBooks>>> EmployeesWithBooks()
        {
            var result = new Result();
            try
            {
                var employeesWithBooks = await _employeeService.EmployeesWithBooks();
                if (employeesWithBooks == null) { return NotFound(); }
                result.Data = employeesWithBooks;
                result.isSuccess = true;
                result.Message = "employees and books they have given";
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpPut("/admin/promoteToAdmin")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<object> PromoteToAdmin(int id)
        {
            var result = new Result();
            try
            {
                var promoteToAdmin = await _employeeService.PromoteToAdmin(id);
                if(promoteToAdmin==null)
                {
                    return NotFound();
                }
                if(promoteToAdmin is string message)
                {
                    result.Message = message;
                    result.isSuccess = false;
                    return BadRequest(result);
                }
                result.Data = promoteToAdmin;
                result.Message = "the role of employee successfully changed to admin";
                result.isSuccess = true;
                return Ok(result);
            }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}
