﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebSpa.Controllers
{
    public class ReservaController : Controller
    {
        [HttpGet]
        public ActionResult AgregarReserva()
        {
            return View();
        }
    }
}