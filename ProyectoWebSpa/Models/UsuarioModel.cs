using Newtonsoft.Json;
using ProyectoWebSpa.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ProyectoWebSpa.Models
{
    public class UsuarioModel
    {
        
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/IniciarSesion";
                JsonContent body = JsonContent.Create(entidad);//Serializar

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;

                }
                return null;
            }

        }
    

    

       public int RegistrarUsuario(UsuarioEnt entidad)
    {
        using (var client = new HttpClient())
        {
            string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);//Serializar

            HttpResponseMessage resp = client.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
            {
                return resp.Content.ReadFromJsonAsync<int>().Result;

            }

            return 0;
        }
    }


        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarUsuarios";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;

                }
                return new List<UsuarioEnt>();
            }

        }

        public int CambiarEstado(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/CambiarEstado";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                JsonContent body = JsonContent.Create(entidad);//Serializar

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;

                }

                return 0;
            }
        }
        public bool RecuperarContrasenna(UsuarioEnt entidad)
    {
        using (var client = new HttpClient())
        {
            string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RecuperarContrasenna";
            JsonContent body = JsonContent.Create(entidad);//Serializar

            HttpResponseMessage resp = client.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
            {
                return resp.Content.ReadFromJsonAsync<bool>().Result;

            }

            return false;
        }
    }
        public UsuarioEnt ConsultarUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarUsuario?q=" + q;
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;

                }
                return null;
            }
        }

        public List<RolEnt> ConsultarRoles()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarRoles";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RolEnt>>().Result;

                }
                return new List<RolEnt>();
            }

        }

        public int EditarUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EditarUsuario";
                string token = HttpContext.Current.Session["TokenUsuario"].ToString();
                JsonContent body = JsonContent.Create(entidad);//Serializar

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;

                }

                return 0;
            }
        }

        public string Encrypt(string toEncrypt)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        string key = ConfigurationManager.AppSettings["secretKey"].ToString();
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
        
    }
}