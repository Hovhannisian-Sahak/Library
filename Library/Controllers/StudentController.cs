using AutoMapper;
using BLL.Service;
using Core.Domains;
using Core.Models;
using FluentValidation;
using IdentityServer4.Services;
using Library.Models.RequestModels;
using Library.Models.ResponseModels;
using Library.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
       
        public StudentController(IStudentService studentService, IMapper mapper, TokenService tokenService)
        {
            _studentService = studentService;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        [HttpPost("signup")]
        public async Task<ActionResult> StudentSignUp(StudentSignupRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new StudentSignupRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var StudentM = _mapper.Map<StudentSignupModel>(request);

                var SignedUpStudent = await _studentService.StudentSignup(StudentM);
                if (SignedUpStudent == null)
                {
                    return NotFound();
                }
                result.Message = "you successfully signed up";
                result.isSuccess = true;
                return StatusCode(StatusCodes.Status201Created,result);
                    }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> StudentLogin(StudentLoginRequest request)
        {
            var result = new Result();
            try
            {
                var validator = new StudentLoginRequestValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var StudentM = _mapper.Map<StudentLoginModel>(request);

                var LoggedInStudent = await _studentService.StudentLogin(StudentM);
                if (LoggedInStudent == null)
                {
                    return NotFound();
                }
                
                string token = _tokenService.CreateToken(LoggedInStudent.Id, "student");
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
                return Ok(result);
        }
            catch (Exception)
            {
                result.isSuccess = false;
                result.Message = "An error occurred on the server";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

      
        [HttpPost("borrowbook")]
        [Authorize]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            var result = new Result();

            try
            {
                    var validator = new BorrowBookRequestValidator();
                    var validationResult = validator.Validate(request);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }
                    var borrowbookM = _mapper.Map<BorrowBookModel>(request);
                    var bookToBorrow = await _studentService.BorrowBook(borrowbookM);
                    if (bookToBorrow == null)
                    {
                        return NotFound();
                    }
                    if (bookToBorrow is FailToBorrowResult res)
                    {
                        result.Message = res.Message;
                        result.isSuccess = res.IsSuccess;
                        return BadRequest(result);
                    }
                    result.Data = bookToBorrow;
                    result.Message = "book was successfully borrowed";
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

        [HttpPost("deliverbook")]
        [Authorize]
        public async Task<IActionResult> DeliverBook([FromBody] DeliverBookRequest request)
        {
            var result = new Result();
            try
            {
                var studentId = _tokenService.GetBearerTokenFromCookie(HttpContext);
                if (request.StudentId == studentId)
                {
                    var validator = new DeliverBookRequestValidator();
                    var validationResult = validator.Validate(request);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }
                    var deliverBookM = _mapper.Map<DeliverBookModel>(request);
                    var bookToDeliver = await _studentService.DeliverBook(deliverBookM);
                    if (bookToDeliver == null)
                    {
                        return NotFound(); 
                    }
                    if (bookToDeliver is FailToBorrowResult res)
                    {
                        result.Message = res.Message;
                        result.isSuccess = res.IsSuccess;
                        return BadRequest(result);
                    }
                    result.Data = bookToDeliver;
                    result.Message = "book was successfully delivered";
                    result.isSuccess = true;
                    return Ok(result);

                }
                return Unauthorized();
             
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