using Datos.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Implementacion
{
    public class PacienteAplicacion
    {
        public HttpResponseMessage Listar()
        {
            HttpResponseMessage respuesta = null;

            string urlBase = ConfigurationManager.AppSettings["rutaAPI"];

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(urlBase);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            respuesta = httpClient.GetAsync("api/Paciente").Result;

            return respuesta;
        }

        public void Guardar(PacienteDto dto)
        {
            string urlBase = ConfigurationManager.AppSettings["rutaAPI"];

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(urlBase);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "UTF-8";

            httpClient.PostAsync("api/Paciente", content);
        }
    }
}
