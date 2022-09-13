using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<Usuarios>> SearchListAsync();

    }
}
