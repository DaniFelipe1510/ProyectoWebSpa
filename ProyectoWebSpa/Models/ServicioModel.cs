﻿using ProyectoWebSpa.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace ProyectoWebSpa.Models
{
    public class ServicioModel
    {
        public List<ServicioEnt> ConsultarServicios()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarServicios";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ServicioEnt>>().Result;
                }

                return new List<ServicioEnt>();
            }
        }

        public long RegistrarServicio(ReservaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarServicio";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                JsonContent body = JsonContent.Create(entidad);//Serializar

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<long>().Result;

                }

                return 0;
            }
        }
    }
}