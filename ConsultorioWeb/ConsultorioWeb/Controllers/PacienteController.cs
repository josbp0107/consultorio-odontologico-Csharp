using Aplicacion.Implementacion;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Aplicacion.Dtos;

namespace ConsultorioWeb.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteAplicacion pacienteAplicacion = new PacienteAplicacion();

        //GET paciente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listado()
        {
            List<PacienteDto> listado = new List<PacienteDto>();
            HttpResponseMessage respuesta = pacienteAplicacion.Listar();

            var pacientes = respuesta.Content.ReadAsStringAsync().Result;

            try
            {
                listado = JsonConvert.DeserializeObject<List<PacienteDto>>(pacientes);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            ViewBag.pacientes = listado;
            return PartialView();
        }

        public void GuardarPaciente(PacienteDto dto)
        {
            pacienteAplicacion.Guardar(dto);
        }
    }
}