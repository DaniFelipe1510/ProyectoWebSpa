﻿using ProyectoWebSpa.Entities;
using ProyectoWebSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebSpa.Controllers
{
    public class CarritoController : Controller
    {
        CarritoModel model = new CarritoModel();
        ProductoModel modelProductos = new ProductoModel();

        [HttpGet]
        public ActionResult AgregarCursoCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt();

            entidad.FechaCarrito = DateTime.Now;
            entidad.IdProducto = q;
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            entidad.CantidadArticulos = 1;
            var respuesta = model.AgregarCursoCarrito(entidad);

            var datos = model.ConsultarCursoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadCursos"] = datos.Count();
            Session["SubTotalCursos"] = datos.Sum(x => x.Precio*x.CantidadArticulos);

            //return RedirectToAction("Inicio", "Home");
            if (respuesta > 0)
            {
                return RedirectToAction("Inicio", "Home");
            }
            else
            {
                var productos = modelProductos.ConsultarProductos();
                ViewBag.MsjPantalla = "No hay disponibilidad del producto en este momento";
                return View("../Home/Inicio", productos);
            }
        }

        [HttpGet]
        public ActionResult VerCarrito()
        {
            var datos = model.ConsultarCursoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }

        [HttpGet]
        public ActionResult VerMisProductos()
        {
            var datos = model.ConsultarProductosUsuario(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }

        [HttpPost]
        public ActionResult ConfirmarPago()
        {

            CarritoEnt entidad = new CarritoEnt();
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());

            model.PagarCursosCarrito(entidad);
            return RedirectToAction("Inicio", "Home");
        }

        [HttpGet]
        public ActionResult RemoverCursoCarrito(long q)
        {
            var respuesta = model.RemoverCursoCarrito(q);
            ActualizarDatosSesion();



            if (respuesta > 0)
            {
                return RedirectToAction("VerCarrito", "Carrito");
            }
            else
            {
                ViewBag.MsjPantalla = "No se pudo remover el curso de su carrito";
                return View("VerCarrito");
            }
        }
        public void ActualizarDatosSesion()
        {
            var datos = model.ConsultarCursoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadCursos"] = datos.Count();
            Session["SubTotalCursos"] = datos.Sum(x => x.Precio);
            Session["TotalCursos"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
        }
    }
}