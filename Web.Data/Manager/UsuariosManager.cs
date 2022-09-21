﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Data.Manager
{
    public class UsuariosManager : BaseManager<Usuarios>
    {

        public async override Task<bool> Delete(Usuarios entityModel)
        {
            entityModel.Activo = false;
            contextSingleton.Entry<Usuarios>(entityModel).State = EntityState.Modified;

            var result = await contextSingleton.SaveChangesAsync() > 0;
            contextSingleton.Entry(entityModel).State = EntityState.Detached;

            return result;
        }

        public async override Task<List<Usuarios>> SearchListAsync(Usuarios entityModel)
        {
            return await contextSingleton.Usuarios
                .Where(u => u.Activo == true).Include(x => x.Roles)
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