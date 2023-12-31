﻿using ProyectoWebSpa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebSpa.Models;

namespace ProyectoWebSpa.Controllers
{
    public class ReservaController : Controller
    {
        ServicioModel model = new ServicioModel();
        [HttpGet]
        public ActionResult AgregarReserva(long q, decimal precioServicio)
        {
            ReservaEnt entidad = new ReservaEnt();
            entidad.IdServicio = q;
            entidad.PrecioPago = precioServicio;
    
            return View(entidad);
        }

        [HttpPost]
        public ActionResult RegistrarReserva(ReservaEnt entidad)
        {

            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            var resp = model.RegistrarServicio(entidad);
                //ActualizarDatosSesion();

                if (resp > 0)
                    return RedirectToAction("ConsultarServicios", "Servicio");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Registro");
                }
                
            
        }

        [HttpGet]
        public ActionResult VerMisReservas()
        {
            var datos = model.ConsultarMisReservas(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }


        //public void ActualizarDatosSesion()
        //{
        //    var datos = model.RegistrarServicio();
        //    Session["CantidadCursos"] = datos.Count();
        //    Session["SubTotalCursos"] = datos.Sum(x => x.Precio);
        //    Session["TotalCursos"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
        //}
    }
}