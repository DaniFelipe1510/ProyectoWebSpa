using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebSpa.Entities
{
    public class ReservaEnt
    {
        public long IdReserva { get; set; }
        public long IdServicio { get; set; }

        public long IdUsuario { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public decimal PrecioPago { get; set; }

        public string Cliente { get; set; }
    }
}