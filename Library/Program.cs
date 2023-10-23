using BLL.Service;
using Core.Domains;
using Core.Repositories;
using IdentityServer4.Services;
using Library.MappingProfile.cs;
using Library.Models.ResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
{
    options.UseSqlServer("Scaffold-DbContext \"Server=SAHAK\\SQLEXPRESS;Database=LibraryManagementDB;Trusted_Connection=True;TrustServerCertificate=True;\" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domains\r\n");
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my custom Secret key for authentication")),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role,"Admin"));
    options.AddPolicy("EmployeeOnly", policy =>
       policy.RequireClaim(ClaimTypes.Role, "Employee"));
});
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", policy =>
//    {
//        policy.RequireAssertion(context =>
//        {
//            var httpContext = context.Resource as HttpContext;
//            if (httpContext != null)
//            {
//                var token = httpContext.Request.Cookies["AuthToken"]; // Replace with the name of your token cookie
//                // Extract the user's role from the token claims
//                var userRole = tokenService.ExtractUserRoleFromToken(token);

//                // Check if the user has the "Admin" role
//                if (userRole == "Admin")
//                {
//                    return true;
//                }
//            }
//            return false;
//        });
//    });

//    options.AddPolicy("Employee", policy =>
//    {
//        policy.RequireAssertion(context =>
//        {
//            var httpContext = context.Resource as HttpContext;
//            if (httpContext != null)
//            {
//                var token = httpContext.Request.Cookies["AuthToken"]; // Replace with the name of your token cookie
//                // Extract the user's role from the token claims
//                var userRole = tokenService.ExtractUserRoleFromToken(token);

//                // Check if the user has the "Employee" role
//                if (userRole == "Employee")
//                {
//                    return true;
//                }
//            }
//            return false;
//        });
//    });
//});
builder.Services.AddDbContext<LibraryManagementDbContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<Result>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
