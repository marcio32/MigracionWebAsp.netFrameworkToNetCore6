using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public ProductosController(IHttpClientFactory httpClientFactory) =>
        _httpClient = httpClientFactory;

        public IActionResult Productos()
        {
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
            var productos = await baseApi.LoginToApi("Productos/GuardarProducto", producto);
            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }

        public async Task<IActionResult> GuardarProducto(Productos producto)
        {
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
            var productos = await baseApi.LoginToApi("Productos/GuardarProducto", producto);

            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }

        public async Task<IActionResult> EliminarProducto([FromBody] Productos producto)
        {
            producto.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var productos = await baseApi.LoginToApi("Productos/EliminarProducto", producto);

            return await Task.Run(() => View("~/Views/Productos/Productos.cshtml"));
        }
    }
}
