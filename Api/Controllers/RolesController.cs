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
    public class RolesController
    {
        [HttpGet]
        [Route("BuscarRoles")]
        public async Task<List<Roles>> BuscarRoles()
        {
            var searchBranches = new RolesService();
            return await searchBranches.SearchListAsync();

        }

        [HttpPost]
        [Route("GuardarRol")]
        public async Task<List<Roles>> GuardarRol(Roles rol)
        {
            var searchBranches = new RolesService();
            return await searchBranches.SaveRolAsync(rol);

        }

        [HttpPost]
        [Route("EliminarRol")]
        public async Task<List<Roles>> EliminarRol(Roles rol)
        {
            var searchBranches = new RolesService();
            return await searchBranches.DeleteRolAsync(rol);

        }
    }
}
