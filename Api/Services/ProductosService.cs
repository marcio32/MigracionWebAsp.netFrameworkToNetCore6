using Api.Interfaces;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;
using Web.Data.Manager;

namespace Api.Services
{
    public class ProductosService : IProductosService
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        private readonly ProductosManager _manager;

        private readonly ProductosManager dataAccess;

        public ProductosService()
        {
            this._manager = new ProductosManager();
            this.dataAccess = new ProductosManager();
            this.Status = true;
        }
        public async Task<List<Productos>> SearchListAsync()
        {

            try
            {
                var result = await this._manager.SearchListAsync(new Productos());
                return result;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Productos>> SaveProductoAsync(Productos usuarios)
        {

            try
            {
                var isNew = usuarios.Id == 0 ? false : true;
                var result = await this._manager.Save(usuarios, usuarios.Id);
                return await this._manager.SearchListAsync(new Productos()); ;

            }
            catch (Exception ex)
            {
                ProcessError(ex);
                return null;
            }
        }

        public async Task<List<Productos>> DeleteProductoAsync(Productos usuarios)
        {

            try
            {
                var result = await this._manager.Delete(usuarios);
                return await this._manager.SearchListAsync(new Productos()); ;

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
            LogHelper.LogError(ex, "ProductosModel");
        }
    }
}