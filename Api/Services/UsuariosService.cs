using Api.Interfaces;
using Common.Helpers;
using WebFinal.Data;
using WebFinal.Data.Entities;
using WebFinal.Data.Manager;

namespace Api.Services
{
    public class UsuariosService : IUsuariosService
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

        public void ProcessError(Exception ex)
        {
            this.Status = false;
            this.ErrorMessage = ex.Message;
            LogHelper.LogError(ex, "UsuariosModel");
        }
    }
}
