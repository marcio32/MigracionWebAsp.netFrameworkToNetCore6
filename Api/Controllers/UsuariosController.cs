using Api.Interfaces;
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
    public class UsuariosController
    {

        [HttpGet("BuscarUsuarios", Name = "BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            var searchBranches = new UsuariosService();
            return await searchBranches.SearchListAsync();

        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public async Task<List<Usuarios>> SaveUser(Usuarios usuario)
        {
            var searchBranches = new UsuariosService();
            return await searchBranches.SaveUserAsync(usuario);

        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<List<Usuarios>> DeleteUser(Usuarios usuario)
        {
            var searchBranches = new UsuariosService();
            return await searchBranches.DeleteUserAsync(usuario);

        }
    }
}

