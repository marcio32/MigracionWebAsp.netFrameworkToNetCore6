using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.Entities;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController
    {
        [HttpGet]
        [Route("BuscarServicios")]
        public async Task<List<Servicios>> BuscarServicios()
        {
            var searchBranches = new ServiciosService();
            return await searchBranches.SearchListAsync();

        }

        [HttpPost]
        [Route("GuardarServicio")]
        public async Task<List<Servicios>> GuardarServicio(Servicios servicio)
        {
            var searchBranches = new ServiciosService();
            return await searchBranches.SaveServicioAsync(servicio);

        }

        [HttpPost]
        [Route("EliminarServicio")]
        public async Task<List<Servicios>> EliminarServicio(Servicios servicio)
        {
            var searchBranches = new ServiciosService();
            return await searchBranches.DeleteServicioAsync(servicio);

        }
    }
}
