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
    public class RolesManager : BaseManager<Roles>
    {

        public async override Task<bool> Delete(Roles entityModel)
        {
            entityModel.Activo = false;
            contextSingleton.Entry<Roles>(entityModel).State = EntityState.Modified;

            var result = await contextSingleton.SaveChangesAsync() > 0;
            contextSingleton.Entry(entityModel).State = EntityState.Detached;

            return result;
        }

        public async override Task<List<Roles>> SearchListAsync(Roles entityModel)
        {
            return await contextSingleton.Roles
                .Where(u => u.Activo == true)
                .ToListAsync();
        }

        public async override Task<Roles> SearchSingle(Roles entityModel)
        {
            return await contextSingleton.Roles
                .Where(m => m.Id == entityModel.Id || entityModel.Id == 0)
                    .FirstOrDefaultAsync();
        }
    }
}

