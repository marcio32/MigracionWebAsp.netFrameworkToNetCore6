using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class ServiciosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ServiciosViewModel(Servicios v)
        {
            var serviciosViewModel = new ServiciosViewModel();
            serviciosViewModel.Id = v.Id;
            serviciosViewModel.Nombre = v.Nombre;
            serviciosViewModel.Activo = v.Activo;
            return serviciosViewModel;
        }
    }
}
