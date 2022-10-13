using Api.Interfaces;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;
using Web.Data.Manager;

namespace Api.Services
{
    public class RecuperarCuentaService 
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly RecuperarCuentaManager _manager;

        private readonly RecuperarCuentaManager dataAccess;

        public RecuperarCuentaService()
        {
            this._manager = new RecuperarCuentaManager();
            this.dataAccess = new RecuperarCuentaManager();
            this.Status = true;
        }

        public bool SaveCodigoCuentaAsync(Usuarios usuarios)
        {

            try
            {
                var isNew = usuarios.Id == 0 ? false : true;
                var result = this._manager.Save(usuarios, usuarios.Id);
                return result.Result;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return false;
            }
        }

   

        public void ProcessError(Exception ex)
        {
            this.Status = false;
            this.ErrorMessage = ex.Message;
            LogHelper.LogError(ex, "ProductosModel");
        }
    }
}