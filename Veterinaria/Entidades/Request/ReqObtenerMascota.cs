using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades.Request
{
    public class ReqObtenerMascota : ReqBase
    {
        public int Mascota_ID;
        public int Id_Persona;
        public string Descripcion;
        public string Nom_Mascota;
        
    }
}