using Api.Interfaces;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;
using Web.Data.Manager;

namespace Api.Services
{
    public class ServiciosService : IServiciosService
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly ServiciosManager _manager;

        private readonly ServiciosManager dataAccess;

        public ServiciosService()
        {
            this._manager = new ServiciosManager();
            this.dataAccess = new ServiciosManager();
            this.Status = true;
        }
        public async Task<List<Servicios>> SearchListAsync()
        {

            try
            {
                var result = await this._manager.SearchListAsync(new Servicios());
                return result;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Servicios>> SaveServicioAsync(Servicios servicios)
        {

            try
            {
                var result = await this._manager.SaveAsync(servicios);
                return await this._manager.SearchListAsync(new Servicios()); ;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Servicios>> DeleteServicioAsync(Servicios servicios)
        {

            try
            {
                var result = await this._manager.DeleteAsync(servicios);
                return result;

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
            LogHelper.LogError(ex, "ServiciosModel");
        }
    }
}
