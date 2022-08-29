using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Usuarios()
        {
            return View();
        }
    }
}
