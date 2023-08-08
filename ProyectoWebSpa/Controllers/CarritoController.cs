using ProyectoWebSpa.Entities;
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
        ProductoModel modelCursos = new ProductoModel();

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
                var cursos = modelCursos.ConsultarCursos();
                ViewBag.MsjPantalla = "No hay disponibilidad del producto en este momento";
                return View("../Home/Inicio", cursos);
            }
        }
    }
}