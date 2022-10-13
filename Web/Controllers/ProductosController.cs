using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web;
using Web.Data.Entities;
using Web.ViewModels;
using Web.Filters;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private string _token;
        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
           
        }
       

        [AuthorizeUsers(Policy = "ADMINISTRADORES")]
        public IActionResult Productos()
        {
            _token = HttpContext.Session.GetString("Token");
            return View("~/Views/Productos/Productos.cshtml");

        }

        [HttpPost]
        public ActionResult ProductosAddPartial([FromBody] Productos producto)
        {
            var usuViewModel = new ProductosViewModel();
            if (producto != null)
                usuViewModel = producto;
            return PartialView("~/Views/Productos/Partial/ProductosAddPartial.cshtml", usuViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProducto(Productos producto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            if (producto.File != null && producto.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    producto.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    producto.Imagen = Convert.ToBase64String(fileBytes);

                }
            }
            producto.File = null;
            var productos = await baseApi.LoginToApi("Productos/GuardarProducto", producto, token);
            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }

        public async Task<IActionResult> GuardarProducto(Productos producto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            if (producto.File != null && producto.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    producto.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    producto.Imagen = Convert.ToBase64String(fileBytes);

                }
            }
            producto.File = null;
            
            var productos = await baseApi.LoginToApi("Productos/GuardarProducto", producto, token);

            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }

        public async Task<IActionResult> EliminarProducto([FromBody] Productos producto)
        {
            var token = HttpContext.Session.GetString("Token");
            producto.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var productos = await baseApi.LoginToApi("Productos/EliminarProducto", producto, token);

            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }
    }
}
