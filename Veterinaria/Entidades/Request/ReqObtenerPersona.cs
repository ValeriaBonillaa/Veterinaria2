using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades.Request
{
    public class ReqObtenerPersona : ReqBase
    {
        public int Id_Persona;
        public int Id_Rol;
        public string Nombre_Completo;
        public string Num_Contacto;
    }
}