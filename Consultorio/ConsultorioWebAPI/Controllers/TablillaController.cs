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
    [RoutePrefix("api/Tablilla")]
    public class TablillaController : ApiController
    {
        public readonly TablillaRepositorio tablillaRepositorio = new TablillaRepositorio();

        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                List<TablillaDto> tablilla = tablillaRepositorio.listarTablilla();
                if (tablilla.Any())
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = tablilla;
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
        public HttpResponseMessage Save([FromBody] TablillaDto tablillaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (tablillaRepositorio.guardarTablilla(tablillaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "tablilla guardada correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "La tablilla no se guardó correctamente" };
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
            }
            return Request.CreateResponse(codigoEstado, informacion, "application/json");
        }

        [HttpPut]
        public HttpResponseMessage Update([FromBody] TablillaDto tablillaDto)
        {
            HttpStatusCode codigoEstado = new HttpStatusCode();
            object informacion = null;

            try
            {
                if (tablillaRepositorio.actualizarTablilla(tablillaDto))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "Los datos de la tablilla han sido actualizados" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al actualizar datos de la tablilla" };                }
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
                if (tablillaRepositorio.eliminarTablilla(id))
                {
                    codigoEstado = HttpStatusCode.OK;
                    informacion = new { error = false, message = "El paciente ha sido eliminado correctamente" };
                }
                else
                {
                    codigoEstado = HttpStatusCode.BadRequest;
                    informacion = new { error = true, message = "Hubo un error al eliminar la tablilla " };
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