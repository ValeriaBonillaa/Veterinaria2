using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capas.Entidades
{
    public class Usuario
    {
        public int? Id_Usuario { get; set; }
        public int? Id_Persona { get; set; }
        public string correo { get; set; }
        public string pass { get; set; }
    }
}