using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public RolesController(IHttpClientFactory httpClientFactory) =>
        _httpClient = httpClientFactory;
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
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/GuardarRol", rol);
            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }

        public async Task<IActionResult> GuardarRol(Roles rol)
        {
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/GuardarRol", rol);

            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }

        public async Task<IActionResult> EliminarRol([FromBody] Roles rol)
        {
            rol.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.LoginToApi("Roles/EliminarRol", rol);

            return await Task.Run(() => View("~/Views/Roles/Roles.cshtml"));
        }
    }
}
