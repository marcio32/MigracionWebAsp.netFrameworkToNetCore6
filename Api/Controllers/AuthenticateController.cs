using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Data.Entities;
using WebFinal.Data;
using WebFinal.Data.Entities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private static ApplicationDbContext contextInstance = null;


        public AuthenticateController(IConfiguration configuration)
        {
            DbContextOptionsBuilder optionsBuilder;
            contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login model)
        {
            var user = contextInstance.Usuarios.Where(x => x.Mail == model.Mail).Include(x => x.Roles).FirstOrDefault();
            if (user != null)
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                authClaims.Add(new Claim(ClaimTypes.Role, user.Roles.Nombre));

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
