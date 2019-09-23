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
    [RoutePrefix("api/Paciente")]
    public class PacienteController : ApiController
    {

        public readonly PacienteRepositorio pacienteRepositorio = new PacienteRepositorio();

        /*
         * Metodo Listar los pacientes que se encuentra en la base de datos
         */
        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;
            try
            {
                List<PacienteDto> pacientes = pacienteRepositorio.listarPaciente();
                if (pacientes.Any())
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = pacientes;
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "No se ha encontrado ningun registro en la base de datos" };
                }
            }
            catch(Exception ex)
            {
                string message_err = ex.Message;
            }

           return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        /*
         * Este metodo por medio de http Post guarda el registro de pacientes
         * @param Se le pasa como parametro la clase PacienteDto donde se encuentra todo los atributos de esta misma
         */
        [HttpPost]
        public HttpResponseMessage Save([FromBody] PacienteDto pacienteDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (pacienteRepositorio.GuardarPaciente(pacienteDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "Informacion del paciente guardada correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Informacion del paciente NO guardada correctamente" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        /*
         *  Update Query
         */
        [HttpPut]
        public HttpResponseMessage Update([FromBody] PacienteDto pacienteDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (pacienteRepositorio.ActualizarPaciente(pacienteDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "Los datos del paciente han sido actualizados" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al actualizar datos del paciente" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        /*
         * Delete Query
         */
        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (pacienteRepositorio.EliminarPaciente(id))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "El paciente ha sido eliminado correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al eliminar el paciente" };
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