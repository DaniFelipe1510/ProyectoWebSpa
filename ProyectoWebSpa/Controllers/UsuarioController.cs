using ProyectoWebSpa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoWebSpa.Entities;
using ProyectoWebSpa.Models;

namespace ProyectoWebSpa.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioModel model = new UsuarioModel();
        //Todos los select tipo httpget
        [HttpGet]
        public void IniciarSesion(UsuarioEnt entidad)
        { 

        }

        //Insert
        [HttpPost]
        public void RegistrarUsuario(UsuarioEnt entidad)
        { 
        }


    }
}
