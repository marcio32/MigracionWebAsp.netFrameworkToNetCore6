using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFinal.Data;
using WebFinal.Data.Entities;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuariosController 
    {

        [HttpGet(Name = "BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            var searchBranches = new UsuariosService();
            return await searchBranches.SearchListAsync();
        }
    }
}
