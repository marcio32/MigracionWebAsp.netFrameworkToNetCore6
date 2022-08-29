using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFinal.Data;
using WebFinal.Data.Entities;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController 
    {

        [HttpGet("BuscarUsuarios", Name = "BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            var searchBranches = new UsuariosService();
            return await searchBranches.SearchListAsync();
        }
    }
}
