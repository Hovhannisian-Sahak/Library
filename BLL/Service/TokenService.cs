using Core.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{

    public class TokenService
    {
        public string CreateToken(int id, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("id", id.ToString()),
            new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public int? GetBearerTokenFromCookie(HttpContext httpContext)
        {
            string tokenCookie = httpContext.Request.Cookies["AuthToken"];
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

    }
}
//public string ExtractUserRoleFromToken(string token)
//{
//    var tokenHandler = new JwtSecurityTokenHandler();
//    var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication"); // Use the same key as in your token generation
//    var validationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false, // You may need to configure issuer validation
//        ValidateAudience = false, // You may need to configure audience validation
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };

//    ClaimsPrincipal principal = null;

//    try
//    {
//        principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
//    }
//    catch (SecurityTokenException)
//    {
//        // Handle token validation errors here, e.g., token is invalid or expired
//    }

//    var roleClaim = principal?.FindFirst(ClaimTypes.Role);

//    if (roleClaim != null)
//    {
//        return roleClaim.Value;
//    }

//    // Return a default role or handle cases where the role claim is not present in the token
//    return "DefaultRole";
//}

//    }
//}
