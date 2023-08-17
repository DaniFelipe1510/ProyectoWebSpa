using ProyectoWebSpa.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace ProyectoWebSpa.Models
{
    public class CarritoModel
    {
        public List<CarritoEnt> ConsultarCursoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarCursoCarrito?q=" + q;
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public List<CarritoEnt> ConsultarProductosUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarProductosUsuario?q=" + q;
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public int AgregarCursoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/AgregarCursoCarrito";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int PagarCursosCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/PagarCursosCarrito";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }
    }
}