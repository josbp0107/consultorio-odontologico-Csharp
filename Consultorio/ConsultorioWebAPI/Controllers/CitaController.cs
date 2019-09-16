using Datos.Dtos;
using Datos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Cita")]
    public class CitaController : ApiController
    {
        public readonly CitaRepositorio citaRepositorio = new CitaRepositorio();

        /*
         * List Cites
         */
        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                List<CitaDto> citas = citaRepositorio.List();
                if (citas.Any())
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = citas;
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

        /*
         * Save Cites
         */
        [HttpPost]
        public HttpResponseMessage Save([FromBody] CitaDto citaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (citaRepositorio.GuardarCita(citaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "Informacion de la cita  ha sido guardada correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al guardar la cita" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }

            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        /*
         * Update Cites
         */
        [HttpPut]
        public HttpResponseMessage Update([FromBody] CitaDto citaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;
            try
            {
                if (citaRepositorio.ActualizarCita(citaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "La cita han sido actualizada" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al actualizar la cita" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        /*
         * Delete Cites
         */
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;
            try
            {
                if (citaRepositorio.EliminarCita(id))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "La cita ha sido eliminado correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al eliminar la cita" };
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