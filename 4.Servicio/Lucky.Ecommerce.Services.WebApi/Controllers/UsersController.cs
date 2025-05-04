using Lucky.Ecommerce.Application.Dto;
using Lucky.Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Lucky.Ecommerce.Services.WebApi.Helpers.AppSettings;
using Lucky.Ecommerce.Transversal.Common;

namespace Lucky.Ecommerce.Services.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersApplication _usersApplication;
        private readonly JWTSettings _jwtSettings;
        public UsersController(IUsersApplication usersApplication, IOptions<JWTSettings> jwtSettings)
        {
            _usersApplication = usersApplication;
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = _usersApplication.Authenticate(loginRequestDto.UserName, loginRequestDto.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    // Modificar las propiedades de la respuesta para que estén en camelCase
                    var result = new
                    {
                        isSuccess = response.IsSuccess,
                        message = response.Message,
                        data = new
                        {
                            userId = response.Data.UserId,
                            firstName = response.Data.FirstName,
                            lastName = response.Data.LastName,
                            userName = response.Data.UserName,
                            token = response.Data.Token
                        }
                    };
                    return Ok(result); // Retorna la respuesta en camelCase
                }
                else
                    return NotFound(response.Message);
            }

            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UsersDto> usersDto)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while generating JWT token", ex);
            }
        }
    }
}
