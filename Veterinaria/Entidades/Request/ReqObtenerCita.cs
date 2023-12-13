using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades.Request
{
    public class ReqObtenerCita : ReqBase
    {
        public int Id_Cita;
        public int Id_Persona;
        public string Fecha_Cita;
    }
}