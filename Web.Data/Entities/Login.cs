using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Entities
{
    public class Login
    {
        [Required(ErrorMessage = "El Mail es requerido")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
    }
}
