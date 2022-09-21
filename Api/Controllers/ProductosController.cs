using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.Entities;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController
    {

        [HttpGet("BuscarProductos", Name = "BuscarProductos")]
        public async Task<List<Productos>> BuscarProductos()
        {
            var searchBranches = new ProductosService();
            return await searchBranches.SearchListAsync();

        }

        [HttpPost]
        [Route("GuardarProducto")]
        public async Task<List<Productos>> GuardarProducto(Productos producto)
        {
            var searchBranches = new ProductosService();
            return await searchBranches.SaveProductoAsync(producto);

        }

        [HttpPost]
        [Route("EliminarProducto")]
        public async Task<List<Productos>> EliminarProducto(Productos producto)
        {
            var searchBranches = new ProductosService();
            return await searchBranches.DeleteProductoAsync(producto);

        }
    }
}
