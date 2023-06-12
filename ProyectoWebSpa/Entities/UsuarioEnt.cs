using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebSpa.Entities
{
    public class UsuarioEnt
    {

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string Confirmarcontrasenna { get; set; }
    }
}