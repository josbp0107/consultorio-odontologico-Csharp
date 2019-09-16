using Aplicacion.Implementacion;
using Datos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

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
            List<PacienteDto> pacienteDtos = new List<PacienteDto>();
            HttpResponseMessage respuesta = pacienteAplicacion.Listar();

            var pacienteDto = respuesta.Content.ReadAsStringAsync().Result;

            try
            {

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            ViewBag.paciente = pacienteDtos;
            return PartialView();
        }

        public void GuardarPaciente(PacienteDto dto)
        {
            pacienteAplicacion.Guardar(dto);
        }
    }
}