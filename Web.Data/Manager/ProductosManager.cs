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
    public class ProductosManager : BaseManager<Productos>
    {

        public async override Task<bool> Delete(Productos entityModel)
        {
            entityModel.Activo = false;
            contextSingleton.Entry<Productos>(entityModel).State = EntityState.Modified;

            var result = await contextSingleton.SaveChangesAsync() > 0;
            contextSingleton.Entry(entityModel).State = EntityState.Detached;

            return result;
        }

        public async override Task<List<Productos>> SearchListAsync(Productos entityModel)
        {
            return await contextSingleton.Productos
                .Where(u => u.Activo == true).ToListAsync();
        }

        public async override Task<Productos> SearchSingle(Productos entityModel)
        {
            return await contextSingleton.Productos
                .Where(m => m.Id == entityModel.Id || entityModel.Id == 0)
                    .FirstOrDefaultAsync();
        }
    }
}