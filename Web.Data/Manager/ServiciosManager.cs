using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Data.Manager
{
    public class ServiciosManager : BaseManager<Servicios>
    {

        public async Task<List<Servicios>> DeleteAsync(Servicios entityModel)
        {
            contextSingleton.Database.ExecuteSqlRaw($"DeleteServicio {entityModel.Id}");
            return contextSingleton.Servicios.FromSqlRaw("GetServicios").ToList(); 
        }

        public async override Task<List<Servicios>> SearchListAsync(Servicios entityModel)
        {
            return contextSingleton.Servicios.FromSqlRaw("GetServicios").ToList();
        }

        public async override Task<Servicios> SearchSingle(Servicios entityModel)
        {
            return await contextSingleton.Servicios
                .Where(m => m.Id == entityModel.Id || entityModel.Id == 0)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Servicios>> SaveAsync(Servicios entityModel)
        {
             contextSingleton.Database.ExecuteSqlRaw($"SaveOrUpdateServicios {entityModel.Id}, {entityModel.Nombre}, {entityModel.Activo}");

            return contextSingleton.Servicios.FromSqlRaw("GetServicios").ToList();
        }

        public override Task<bool> Delete(Servicios entityModel)
        {
            throw new NotImplementedException();
        }
    }
}

