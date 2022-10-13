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
using Web.Data;
using Web.Data.Entities;
using Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperarCuentaController : ControllerBase
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly IConfiguration _configuration;

        private static ApplicationDbContext contextInstance;


        public RecuperarCuentaController(IConfiguration configuration)
        {
            contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }
    

        [HttpPost]
        [Route("GuardarCodigo")]
        public bool Codigo([FromBody] Login model)
        {
            try
            {
                var service = new RecuperarCuentaService();
                var user = contextInstance.Usuarios.Where(x => x.Mail == model.Mail).FirstOrDefault();
                if (user != null)
                {
                    user.Codigo = (int)model.Codigo;
                    return service.SaveCodigoCuentaAsync(user);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpPost]
        [Route("CambiarClave")]
        public bool CambiarClave([FromBody] Login model)
        {
            try
            {
                var service = new RecuperarCuentaService();
                var user = contextInstance.Usuarios.Where(x => x.Mail == model.Mail && x.Codigo == model.Codigo).FirstOrDefault();
                if (user != null)
                {
                    user.Codigo = (int)model.Codigo;
                    user.Clave = model.Password;
                    return service.SaveCodigoCuentaAsync(user);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
