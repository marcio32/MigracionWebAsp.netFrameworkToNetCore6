using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(Roles v)
        {
            var rolesViewModel = new RolesViewModel();
            rolesViewModel.Id = v.Id;
            rolesViewModel.Nombre = v.Nombre;
            rolesViewModel.Activo = v.Activo;
            return rolesViewModel;
        }
    }
}
