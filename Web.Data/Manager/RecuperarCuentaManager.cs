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
    public class RecuperarCuentaManager : BaseManager<Usuarios>
    {
        public override Task<bool> Delete(Usuarios entityModel)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Usuarios>> SearchListAsync(Usuarios entityModel)
        {
            throw new NotImplementedException();
        }

        public async override Task<Usuarios> SearchSingle(Usuarios entityModel)
        {
            return await contextSingleton.Usuarios
                .Where(m => m.Id == entityModel.Id || entityModel.Id == 0)
                    .FirstOrDefaultAsync();
        }

    }
}