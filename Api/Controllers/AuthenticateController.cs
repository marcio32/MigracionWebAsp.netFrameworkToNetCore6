using Common.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly IConfiguration _configuration;

        private static ApplicationDbContext contextInstance;


        public AuthenticateController(IConfiguration configuration)
        {
            contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login model)
        {
            try
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
            catch (Exception ex)
            {
                ProcessError(ex);
                return StatusCode(StatusCodes.Status404NotFound, "Ocurrio un error por favor contacte a sistemas");
            }

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            try
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
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async void ProcessError(Exception ex)
        {
            this.Status = false;
            this.ErrorMessage = ex.Message;
            await LogHelper.LogError(ex, "AuthenticateController");
        }
    }
}
