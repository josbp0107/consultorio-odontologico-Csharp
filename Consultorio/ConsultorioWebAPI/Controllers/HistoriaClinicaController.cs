using Datos.Dtos;
using Datos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/HistoriaClinica")]
    public class HistoriaClinicaController : ApiController
    {
        public readonly HistoriaClinicaRepositorio historiaClinicaRepositorio = new HistoriaClinicaRepositorio();

        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                List<HistoriaClinicaDto> historiaClinica = historiaClinicaRepositorio.listarHistorias();
                if (historiaClinica.Any())
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = historiaClinica;
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "No se ha encontrado ningun registro en la base de datos" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }

            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody] HistoriaClinicaDto historiaClinicaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (historiaClinicaRepositorio.GuardarHistoriaClinica(historiaClinicaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "La historia clinica se ha  guardado correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "No se guardó correctamente la historia clinica" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        [HttpPut]
        public HttpResponseMessage Update([FromBody] HistoriaClinicaDto historiaClinicaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (historiaClinicaRepositorio.ActualizarHistoriaClinica(historiaClinicaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "La historia clinica ha sido actualizados" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al actualizar la historia clinica" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (historiaClinicaRepositorio.EliminarHistorial(id))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "La historia clinica ha sido eliminado correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al eliminar la historia clinica" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }

            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }
    }
}