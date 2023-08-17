using ProyectoWebSpa.Entities;
using ProyectoWebSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebSpa.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            var datos = model.ConsultarUsuarios();
            return View(datos);
        }

        [HttpGet]
        public ActionResult CambiarEstado(long q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = q;

            var resp = model.CambiarEstado(entidad);

            if (resp > 0)
            {
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            }

            else
            {
                ViewBag.MsjPantalla = "No se ha podido cambiar el estado del usuario";
                return View("ConsultarUsuarios");
            }


        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = model.ConsultarUsuario(q);
            var roles = model.ConsultarRoles();

            var ComboRoles = new List<SelectListItem>();
            foreach (var item in roles)
            {
                ComboRoles.Add(new SelectListItem
                {
                    Text = item.NombreRol,
                    Value = item.IdRol.ToString()
                });
            }
            ViewBag.Combo = ComboRoles;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarUsuario(UsuarioEnt entidad)
        {
            var resp = model.EditarUsuario(entidad);

            if (resp > 0)
            {
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            }

            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("Editar");
            }
        }

    }
}