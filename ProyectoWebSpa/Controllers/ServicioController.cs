using ProyectoWebSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebSpa.Controllers
{
    public class ServicioController : Controller
    {
        ServicioModel modelServicios = new ServicioModel();

        [HttpGet]
        public ActionResult ConsultarServicios()
        {
            var servicio = modelServicios.ConsultarServicios();
            return View(servicio);
        }
    }
}