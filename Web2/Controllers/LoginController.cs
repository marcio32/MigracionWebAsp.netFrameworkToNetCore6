using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.ContentModel;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;
using Web.Data.Base;
using Web.Data.Dtos;
using Web.Data.Entities;
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
                storage.Store("Token", okResult.Value);
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }
    }
}
