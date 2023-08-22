using ProyectoWebSpa.Entities;
using ProyectoWebSpa.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

        [HttpGet]
        public ActionResult ConsultarMantServicios()
        {
            var cursos = modelServicios.ConsultarServicios();
            return View(cursos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarServicio(ServicioEnt entidad, HttpPostedFileBase Imagen)
        {
            //Registramos el curso sin imagen, para obtner el IDCurso
            entidad.Imagen = string.Empty;
            var resp = modelServicios.RegistrarMantServicio(entidad);

            //Guardamos la imagen en una carpeta del proyecto, con el IDCurso correspondiente
            string extension = Path.GetExtension(Path.GetFileName(Imagen.FileName));
            string ruta = ConfigurationManager.AppSettings["rutaMoverImagenes"] + resp + extension;
            Imagen.SaveAs(ruta);

            //Actualizamos el curso para ponerle la ruta de donde guardamos la imagenn
            entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + resp + extension;
            entidad.IdServicio = resp;
            modelServicios.ActualizarRuta(entidad);

            return RedirectToAction("ConsultarMantServicios", "Servicio");
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = modelServicios.ConsultarServicio(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarCurso(ServicioEnt entidad, HttpPostedFileBase ImagenCurso)
        {


            string extension = Path.GetExtension(Path.GetFileName(ImagenCurso.FileName));
            int tamanno = ImagenCurso.ContentLength;

            if ((extension == ".png" || extension == ".jpg" || extension == ".jpeg") && (tamanno <= 50000))
            {
                System.IO.File.Delete(ConfigurationManager.AppSettings["rutaEliminarImagenes"] + entidad.Imagen);
                //Actualizamos el curso sin imagen, para obtener el IDCurso
                entidad.Imagen = string.Empty;
                modelServicios.ActualizarServicio(entidad);

                //Guardamos la imagen en una carpeta del proyecto, con el IDCurso correspondiente

                string ruta = ConfigurationManager.AppSettings["rutaMoverImagenes"] + entidad.IdServicio + extension;
                ImagenCurso.SaveAs(ruta);

                //Actualizamos el curso para ponerle la ruta de donde guardamos la imagenn
                entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + entidad.IdServicio + extension;
                modelServicios.ActualizarRuta(entidad);

                return RedirectToAction("ConsultarMantServicios", "Servicio");
            }
            else
            {
                ViewBag.MsjPantalla = "El formato de la imagen no es permitido";
                return View("Editar");
            }

        }
    }
}