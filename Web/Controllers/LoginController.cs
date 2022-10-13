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
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Web.Models;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly SmtpClient _smptClient;
        public IConfiguration _configuration { get; set; }
        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory;
            _configuration = configuration;
            _smptClient = new SmtpClient();
        }

        public IActionResult Login()
        {
            if (TempData["ErrorLogin"] != null)
                ViewBag.ErrorLogin = TempData["ErrorLogin"].ToString();
            return View("~/Views/Login/Login.cshtml");
        }

        public IActionResult OlvidoClave()
        {
            return View();
        }

        public IActionResult RecuperarClave()
        {
            return View();
        }


        public async Task<IActionResult> EnviarMail(Login model)
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            var random = new Random(seed);
            var codigo = random.Next(000000, 999999);
            model.Codigo = codigo;
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.LoginToApi("RecuperarCuenta/GuardarCodigo", model, "");
            var okResult = response as OkObjectResult;

            if (okResult != null)
            {
                MailMessage mail = new();
                string user = _configuration["ConfiguracionMail:usuarioGmail"];
                string to = _configuration["ConfiguracionMail:toEmail"];
                string password = _configuration["ConfiguracionMail:claveGmail"];
                string host = _configuration["ConfiguracionMail:host"];
                int port = int.Parse(_configuration["ConfiguracionMail:port"]);
                bool ssl = bool.Parse(_configuration["ConfiguracionMail:ssl"]);
                bool defaultCredentials = bool.Parse(_configuration["ConfiguracionMail:defaultCredentialGmail"]);

                string bodyEmail = BodyEmailLogin(codigo);

                mail.From = new MailAddress(user);
                mail.To.Add(new MailAddress(to));
                mail.Subject = "Codigo Recuperacion";
                mail.Body = bodyEmail;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                _smptClient.Host = host;
                _smptClient.Port = port;
                _smptClient.EnableSsl = ssl;
                _smptClient.UseDefaultCredentials = defaultCredentials;

                _smptClient.Credentials = new NetworkCredential(user, password);

                _smptClient.Send(mail);

                return RedirectToAction("RecuperarClave", "Login");
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> CambiarClave(Login model)
        {

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.LoginToApi("RecuperarCuenta/CambiarClave", model, "");
            var okResult = response as OkObjectResult;

            if (okResult != null)
            {


                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        private static string BodyEmailLogin(int codigo)
        {
            string line = "<br>";
            string message = $"<strong>A continuacion se mostrara un codigo que debera ingresar en la web</strong>{line}";
            message += $"{codigo} {line}";
            message += "<ul>";
            message += "</ul>";
            return message;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Login model)
        {
            var storage = new LocalStorage();

            var baseApi = new BaseApi(_httpClient);
            model.Password = EncryptHelper.Encrypt(model.Password);
            var response = await baseApi.LoginToApi("Authenticate/Login", model, "");
            var okResult = response as OkObjectResult;
            var recuerdame = 24;
            if (okResult != null)
            {
                if (model.Recuerdame == true)
                {
                    recuerdame = 100000000;
                }
                var responseSplit = okResult.Value.ToString().Split(";");
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimUserName = new Claim(ClaimTypes.Name, responseSplit[1]);
                Claim claimRole = new Claim(ClaimTypes.Role, responseSplit[2]);
                Claim claimEmail = new Claim(ClaimTypes.Email, responseSplit[3]);

                identity.AddClaim(claimUserName);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                  
                    ExpiresUtc = DateTime.Now.AddMinutes(recuerdame)
                });


                HttpContext.Session.SetString("Token", responseSplit[0]);

                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = responseSplit[0];
                return RedirectToAction("Index", "Home", inicioViewModel);
            }
            else
            {
                TempData["ErrorLogin"] = "La Contraseña o el mail no coinciden";
                return RedirectToAction("Login", "Login");
            }

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }


    }
}
