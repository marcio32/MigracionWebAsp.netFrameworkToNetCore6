using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.ContentModel;
using System.Configuration;
using System.Net.Http;
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
                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = okResult.Value.ToString();
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }
    }
}
