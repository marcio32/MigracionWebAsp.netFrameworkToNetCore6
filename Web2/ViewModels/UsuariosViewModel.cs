using Hanssens.Net;
using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha_Nacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Clave { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Id_Rol { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Activo { get; set; }
        public Roles Roles { get; set; }
    }
}
