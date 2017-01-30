using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEncrypt
{
    public enum Estatus
    {
        OK, ERROR
    }

    public class Respuesta
    {
        public Estatus status { get; set; }
        public string resultado { get; set; }
        public string error { get; set; }
    }
}
