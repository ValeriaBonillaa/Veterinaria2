using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades
{
    public class Cita
    {
        public int Id_Cita { get; set; }
        public int Id_Persona { get; set; }
        public string Fecha_Cita { get; set; }
    }
}