using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Dtos
{
	public class TokenRequest
	{
        [Required(ErrorMessage = "El Mail es requerido")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
    }
}
