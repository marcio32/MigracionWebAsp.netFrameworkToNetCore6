using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web;
using Web.Data.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Filters;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public UsuariosController(IHttpClientFactory httpClientFactory) =>
        _httpClient = httpClientFactory;

        [AuthorizeUsers]
        public IActionResult Usuarios()
        {
            return View("~/Views/Usuarios/Usuarios.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> UsuariosAddPartial([FromBody] Usuarios usuario)
        {
            var usuViewModel = new UsuariosViewModel();
            var baseApi = new BaseApi(_httpClient);

            var roles = await baseApi.GetToApi("Roles/BuscarRoles");
            var okResult = roles as OkObjectResult;
            if (okResult != null)
            {
                var rolesConvert = JsonConvert.DeserializeObject<List<Roles>>(okResult.Value.ToString());
                List<SelectListItem> lista_Roles = new List<SelectListItem>();
                foreach (var item in rolesConvert)
                {
                    lista_Roles.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString() });
                }
                usuViewModel.Lista_Roles = lista_Roles;
            }
            if (usuario != null)
                usuViewModel = usuario;
            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario);
            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario);

            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }

        public async Task<IActionResult> EliminarUsuario([FromBody] Usuarios usuario)
        {
            usuario.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/EliminarUsuario", usuario);

            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }
    }
}
