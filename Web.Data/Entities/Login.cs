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
        public string? Mail { get; set; }

        public string? Password { get; set; }

        public bool Recuerdame { get; set; }

        public string? Token { get; set; }
        public int Codigo { get; set; }
    }
}
