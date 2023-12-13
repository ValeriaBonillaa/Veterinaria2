using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades.Request
{
    public class ReqObtenerUsuario : ReqBase
    {
        public string Correo;
        public string Pass;
    }
}