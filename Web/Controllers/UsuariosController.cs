using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web;
using Web.Data.Entities;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public UsuariosController(IHttpClientFactory httpClientFactory) =>
        _httpClient = httpClientFactory;

        public IActionResult Usuarios()
        {
            return View("~/Views/Usuarios/Usuarios.cshtml");

        }

        [HttpPost]
        public ActionResult UsuariosAddPartial([FromBody] Usuarios usuario)
        {
            var usuViewModel = new UsuariosViewModel();
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

            return await Task.Run(() => View());
        }
    }
}
