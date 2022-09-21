using Api.Interfaces;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;
using Web.Data.Manager;

namespace Api.Services
{
    public class RolesService : IRolesService
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly RolesManager _manager;

        private readonly RolesManager dataAccess;

        public RolesService()
        {
            this._manager = new RolesManager();
            this.dataAccess = new RolesManager();
            this.Status = true;
        }
        public async Task<List<Roles>> SearchListAsync()
        {

            try
            {
                var result = await this._manager.SearchListAsync(new Roles());
                return result;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Roles>> SaveRolAsync(Roles roles)
        {

            try
            {
                var isNew = roles.Id == 0 ? false : true;
                var result = await this._manager.Save(roles, roles.Id);
                return await this._manager.SearchListAsync(new Roles()); ;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Roles>> DeleteRolAsync(Roles roles)
        {

            try
            {
                var result = await this._manager.Delete(roles);
                return await this._manager.SearchListAsync(new Roles()); ;

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
            LogHelper.LogError(ex, "RolesModel");
        }
    }
}
