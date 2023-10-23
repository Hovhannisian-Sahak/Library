

namespace Library.MappingProfile.cs
{
    using AutoMapper;
    using Core.Models;
    using Library.Models.RequestModels;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddEmployeeRequest, AddEmployeeModel>();
            CreateMap<EmployeeLoginRequest, EmployeeLoginModel>();
            CreateMap<StudentSignupRequest, StudentSignupModel>();
            CreateMap<StudentLoginRequest, StudentLoginModel>();
            CreateMap<AddBookRequest, AddBookModel>();
            CreateMap<EditBookRequest, EditBookModel>();
            CreateMap<EditEmployeeRequest, EditEmployeeModel>();
            CreateMap<BorrowBookRequest,BorrowBookModel>();
            CreateMap<DeliverBookRequest, DeliverBookModel>();
            CreateMap<FilterBooksRequest, FilterBooksModel>();
            CreateMap<FilterEmployeesRequest, FilterEmployeesModel>();
        }
    }
}
