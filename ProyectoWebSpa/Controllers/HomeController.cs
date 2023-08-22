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
        ProductoModel modelProductos = new ProductoModel();
        CarritoModel modelCarrito = new CarritoModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Inicio()
        {
            var datos = modelCarrito.ConsultarCursoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadCursos"] = datos.Count();
            Session["SubTotalCursos"] = datos.Sum(x => x.Precio * x.CantidadArticulos);

            var productos = modelProductos.ConsultarProductos();
            return View(productos);
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

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                entidad.Contrasenna = model.Encrypt(entidad.Contrasenna);
                var resp = model.IniciarSesion(entidad);

                if (resp != null)
                {
                    Session["CorreoUsuario"] = resp.Correo;
                    Session["IdUsuario"] = resp.IdUsuario.ToString();
                    Session["NombreUsuario"] = resp.Nombre;
                    Session["RolUsuario"] = resp.NombreRol;
                    Session["IdRolUsuario"] = resp.IdRol;
                    Session["TokenUsuario"] = resp.Token;
                    
                    return RedirectToAction("Inicio", "Home");
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

        [HttpPost]
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

        [HttpGet]
        public ActionResult VerCarrito()
        {
            ActualizarDatosSesion();



            var productos = modelProductos.ConsultarProductos();
            return View(productos);
        }


        public void ActualizarDatosSesion()
        {
            var datos = modelCarrito.ConsultarCursoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadCursos"] = datos.Count();
            Session["SubTotalCursos"] = datos.Sum(x => x.Precio);
            Session["TotalCursos"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Cambiar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarContrasenna(UsuarioEnt entidad)
        {
            entidad.IdUsuario = long.Parse(Session["IdUusario"].ToString());
            entidad.Correo = Session["CorreoUsuario"].ToString();
            entidad.Contrasenna = model.Encrypt(entidad.Contrasenna);
            var respValidar = model.IniciarSesion(entidad);

            //La contrasenna no es valida
            if (respValidar == null)
            {
                ViewBag.MsjPantalla = "Su contraseña actual no es la correcta";
                return View("Cambiar");

            }

            if (entidad.ContrasennaNueva != entidad.Confirmarcontrasenna)
            {
                ViewBag.MsjPantalla = "Las contraseñas no coinciden";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva == entidad.Contrasenna)
            {
                ViewBag.MsjPantalla = "Debe ingresar una contraseña diferente a la acutal";
                return View("Cambiar");
            }

            entidad.ContrasennaNueva = model.Encrypt(entidad.ContrasennaNueva);

            var respCambiar = model.CambiarContrasenna(entidad);

            if (respCambiar > 0)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido cambiar su contraseña";
                return View("Cambiar");
            }
        }



    }
}