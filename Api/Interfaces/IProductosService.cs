using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosService
    {
        Task<List<Productos>> SearchListAsync();

    }
}
