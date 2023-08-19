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
    public class ProductoController : Controller
    {
        ProductoModel modelProductos = new ProductoModel();

        [HttpGet]
        public ActionResult ConsultarMantProductos()
        {
            var cursos = modelProductos.ConsultarProductos();
            return View(cursos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(ProductoEnt entidad, HttpPostedFileBase ImagenCurso)
        {
            //Registramos el curso sin imagen, para obtner el IDCurso
            entidad.Imagen = string.Empty;
            var resp = modelProductos.RegistrarProducto(entidad);

            //Guardamos la imagen en una carpeta del proyecto, con el IDCurso correspondiente
            string extension = Path.GetExtension(Path.GetFileName(ImagenCurso.FileName));
            string ruta = ConfigurationManager.AppSettings["rutaMoverImagenes"] + resp + extension;
            ImagenCurso.SaveAs(ruta);

            //Actualizamos el curso para ponerle la ruta de donde guardamos la imagenn
            entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + resp + extension;
            entidad.IdProducto = resp;
            modelProductos.ActualizarRuta(entidad);

            return RedirectToAction("ConsultarMantCursos", "Curso");
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = modelProductos.ConsultarProducto(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarProducto(ProductoEnt entidad, HttpPostedFileBase ImagenCurso)
        {


            string extension = Path.GetExtension(Path.GetFileName(ImagenCurso.FileName));
            int tamanno = ImagenCurso.ContentLength;

            if ((extension == ".png" || extension == ".jpg" || extension == ".jpeg") && (tamanno <= 50000))
            {
                System.IO.File.Delete(ConfigurationManager.AppSettings["rutaEliminarImagenes"] + entidad.Imagen);
                //Actualizamos el curso sin imagen, para obtener el IDCurso
                entidad.Imagen = string.Empty;
                modelProductos.ActualizarProducto(entidad);

                //Guardamos la imagen en una carpeta del proyecto, con el IDCurso correspondiente

                string ruta = ConfigurationManager.AppSettings["rutaMoverImagenes"] + entidad.IdProducto + extension;
                ImagenCurso.SaveAs(ruta);

                //Actualizamos el curso para ponerle la ruta de donde guardamos la imagenn
                entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + entidad.IdProducto + extension;
                modelProductos.ActualizarRuta(entidad);

                return RedirectToAction("ConsultarMantCursos", "Curso");
            }
            else
            {
                ViewBag.MsjPantalla = "El formato de la imagen no es permitido";
                return View("Editar");
            }

        }
    }
}