using ProyectoWebSpa.Entities;
using ProyectoWebSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebSpa.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Recuperar()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Perfil()
        {

            return View();
        }
        /*
        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                entidad.Contrasenna = model.Encrypt(entidad.Contrasenna);
                var resp = model.IniciarSesion(entidad);

                if (resp != null)
                {
                    Session["CorreoUsuario"] = resp.CorreoElectronico;
                    Session["IdUsuario"] = resp.IdUsuario.ToString();
                    Session["NombreUsuario"] = resp.Nombre;
                    Session["RolUsuario"] = resp.NombreRol;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.MsjPantalla = "No se ha podido validar su información";
                return View("Login");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                entidad.Contrasenna = model.Encrypt(entidad.Contrasenna);
                entidad.IdRol = 2;
                entidad.Estado = true;

                var resp = model.RegistrarUsuario(entidad);

                if (resp > 0)
                    return RedirectToAction("Login", "Home");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Registro");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioEnt entidad)
        {
            try
            {
                var resp = model.RecuperarContrasenna(entidad);

                if (resp)
                    return RedirectToAction("Login", "Home");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido recuperar su contraseña";
                    return View("Registro");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        */
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}