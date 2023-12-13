using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades
{
    public class Mascota
    {
        public int Mascota_ID { get; set; }
        public int Id_Persona { get; set; }
        public string descripcion { get; set; }
        public string Nom_Mascota { get; set; }
    }
}