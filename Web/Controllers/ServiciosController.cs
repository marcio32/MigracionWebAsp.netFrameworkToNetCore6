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
    public class ServiciosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public ServiciosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }
       

        [AuthorizeUsers(Policy = "ADMINISTRADORES")]
        public IActionResult Servicios()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ServiciosAddPartial([FromBody] Servicios servicio)
        {
          
            var serviciosViewModel = new ServiciosViewModel();
            if (servicio != null)
                serviciosViewModel = servicio;

            return PartialView("~/Views/Servicios/Partial/serviciosAddPartial.cshtml", serviciosViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarServicio(Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = await baseApi.LoginToApi("Servicios/GuardarServicio", servicio, token);
            return await Task.Run(() => View("~/Views/Servicios/Servicios.cshtml"));
        }

        public async Task<IActionResult> GuardarServicio(Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = await baseApi.LoginToApi("Servicios/GuardarServicio", servicio, token);

            return await Task.Run(() => View("~/Views/Servicios/Servicios.cshtml"));
        }

        public async Task<IActionResult> EliminarServicio([FromBody] Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            servicio.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var servicios = await baseApi.LoginToApi("Servicios/EliminarServicio", servicio, token);

            return await Task.Run(() => View("~/Views/Servicios/Servicios.cshtml"));
        }
    }
}
