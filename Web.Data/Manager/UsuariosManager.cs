using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFinal.Data.Base;
using WebFinal.Data.Entities;

namespace WebFinal.Data.Manager
{
    public class UsuariosManager : BaseManager<Usuarios>
    {

        public async override Task<bool> Delete(Usuarios entityModel)
        {
            contextSingleton.ChangeTracker.Clear();
            contextSingleton.Entry(entityModel).State = EntityState.Detached;
            contextSingleton.Usuarios.Remove(entityModel);

            var result = await contextSingleton.SaveChangesAsync() > 0;
            contextSingleton.Entry(entityModel).State = EntityState.Detached;

            return result;
        }

        public async override Task<List<Usuarios>> SearchListAsync(Usuarios entityModel)
        {
            var p =  contextSingleton.Usuarios
                .Where(u => u.Activo == true)
                .ToList();
            return await contextSingleton.Usuarios
                .Where(u => u.Activo == true).Include(x=> x.Roles)
                .ToListAsync();
        }

        public async override Task<Usuarios> SearchSingle(Usuarios entityModel)
        {
            return await contextSingleton.Usuarios
                .Where(m => m.Id == entityModel.Id || entityModel.Id == 0)
                    .FirstOrDefaultAsync();
        }
    }
}
