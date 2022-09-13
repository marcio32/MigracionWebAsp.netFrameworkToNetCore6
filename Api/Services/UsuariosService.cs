using Api.Interfaces;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;
using Web.Data.Manager;

namespace Api.Services
{
    public class UsuariosService :  IUsuariosService 
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly UsuariosManager _manager;

        private readonly UsuariosManager dataAccess;

        public UsuariosService()
        {
            this._manager = new UsuariosManager();
            this.dataAccess = new UsuariosManager();
            this.Status = true;
        }
        public async Task<List<Usuarios>> SearchListAsync()
        {

            try
            {
                var result = await this._manager.SearchListAsync(new Usuarios());
                return result;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Usuarios>> SaveUserAsync(Usuarios usuarios)
        {

            try
            {
                var isNew = usuarios.Id == 0 ? false : true;
                var result = await this._manager.Save(usuarios, true);
                return await this._manager.SearchListAsync(new Usuarios()); ;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Usuarios>> DeleteUserAsync(Usuarios usuarios)
        {

            try
            {
                var result = await this._manager.Delete(usuarios);
                return await this._manager.SearchListAsync(new Usuarios()); ;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public void ProcessError(Exception ex)
        {
            this.Status = false;
            this.ErrorMessage = ex.Message;
            LogHelper.LogError(ex, "UsuariosModel");
        }
    }
}
