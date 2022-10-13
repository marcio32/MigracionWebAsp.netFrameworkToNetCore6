using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IServiciosService
    {
        Task<List<Servicios>> SearchListAsync();
    }
}
