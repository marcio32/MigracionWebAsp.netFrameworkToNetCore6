﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFinal.Data.Entities
{
    public class Usuarios
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Clave { get; set; }
        public string Mail { get; set; }
        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
        public bool Activo { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
