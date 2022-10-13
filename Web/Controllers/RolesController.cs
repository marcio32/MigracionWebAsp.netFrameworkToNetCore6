using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public RolesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }
       

        [AuthorizeUsers(Policy = "ADMINISTRADORES")]
        public IActionResult Roles()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RolesAddPartial([FromBody] Roles rol)
        {
          
            var rolesViewModel = new RolesViewModel();
            if (rol != null)
                rolesViewModel = rol;

            return PartialView("~/Views/Roles/Partial/rolesAddPartial.cshtml", rolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRol(Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/GuardarRol", rol, token);
            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }

        public async Task<IActionResult> GuardarRol(Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/GuardarRol", rol, token);

            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }

        public async Task<IActionResult> EliminarRol([FromBody] Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            rol.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/EliminarRol", rol, token);

            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }
    }
}
