using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web.ViewModels;
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

        public ActionResult UsuariosAddPartial([FromBody] Usuarios usuario)
        {
            ModelState.Clear();
            var usuViewModel = new UsuariosViewModel();
            usuViewModel.Apellido = "Pruebo";
            usuViewModel.Nombre = "Pruebo";

            return View("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario([FromBody] Usuarios usuario, bool termino)
        {
            if (termino)
            {
                var baseApi = new BaseApi(_httpClient);
                var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario);
            }
            else
            {
                ViewBag.nombre = usuario.Nombre;
            }
         

            return await Task.Run(() => View());
        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.LoginToApi("Usuarios/GuardarUsuario", usuario);
            
            return await Task.Run(() => View());
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
