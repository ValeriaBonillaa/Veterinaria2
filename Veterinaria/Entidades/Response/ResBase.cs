using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Entidades.Response
{
    public class ResBase
    {
        public bool result { set; get; }
        public List<String> listaDeErrores { set; get; }
    }
}