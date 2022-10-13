using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web;
using Web.Data.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Filters;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using Common.Helpers;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public UsuariosController(IHttpClientFactory httpClientFactory)
        {

            _httpClient = httpClientFactory;

        }


        [AuthorizeUsers(Policy = "ADMINISTRADORES")]
        public IActionResult Usuarios()
        {
            return View("~/Views/Usuarios/Usuarios.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> UsuariosAddPartial([FromBody] Usuarios usuario)
        {

            var usuViewModel = new UsuariosViewModel();
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            var roles = await baseApi.GetToApi("Roles/BuscarRoles", token);
            var okResult = roles as OkObjectResult;
            if (usuario != null)
            {
                usuario.Clave = EncryptHelper.Decrypt(usuario.Clave);
                usuViewModel = usuario;
            }

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

            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuarios usuario)
        {
            var token = HttpContext.Session.GetString("Token");
            usuario.Clave = EncryptHelper.Encrypt(usuario.Clave);
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario, token);
            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            usuario.Clave = EncryptHelper.Encrypt(usuario.Clave);
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario, token);

            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }

        public async Task<IActionResult> EliminarUsuario([FromBody] Usuarios usuario)
        {
            var token = HttpContext.Session.GetString("Token");
            usuario.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/EliminarUsuario", usuario, token);

            return await Task.Run(() => View("~/Views/Usuarios/Usuarios.cshtml"));
        }
    }
}
