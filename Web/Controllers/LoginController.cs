using Hanssens.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.ContentModel;
using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using Web.Data.Base;
using Web.Data.Dtos;
using Web.Data.Entities;
using Web.ViewModels;
using Xunit;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public LoginController(IHttpClientFactory httpClientFactory) =>
        _httpClient = httpClientFactory;



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Login model)
        {
            var storage = new LocalStorage();
          
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.LoginToApi("Authenticate/Login", model);
            var okResult = token as OkObjectResult;
            
            if (okResult != null)
            {
                //DEBEMOS CREAR UNA IDENTIDAD (name y role)
                //Y UN PRINCIPAL
                //DICHA IDENTIDAD DEBEMOS COMBINARLA CON LA COOKIE DE 
                //AUTENTIFICACION
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //TODO USUARIO PUEDE CONTENER UNA SERIE DE CARACTERISTICAS
                //LLAMADA CLAIMS.  DICHAS CARACTERISTICAS PODEMOS ALMACENARLAS
                //DENTRO DE USER PARA UTILIZARLAS A LO LARGO DE LA APP
                Claim claimUserName = new Claim(ClaimTypes.Name, "Marcio");
                Claim claimRole = new Claim(ClaimTypes.Role, "Administrador");
                Claim claimIdUsuario = new Claim("IdUsuario", "1");
                Claim claimEmail = new Claim("EmailUsuario", model.Mail);

                identity.AddClaim(claimUserName);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimIdUsuario);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(45)
                });

                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = okResult.Value.ToString();
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("~/Views/Login/Login.cshtml");
        }
    }
}
