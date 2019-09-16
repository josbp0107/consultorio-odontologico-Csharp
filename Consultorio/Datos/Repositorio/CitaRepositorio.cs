using Datos.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Repositorio
{
    public class CitaRepositorio
    {
        /*
         * Metodo para guardar una cita a un paciente en la base de datos
         * @return false devuelve false el cual quiere decir que no se hizo el registro correctamente
         * @return true devuelve verdadero donde el registro fue realizado correctamente
         * @param citaDto atributos de la clase CitaDto
         */
        public bool GuardarCita(CitaDto citaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_citas", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 1);
                    sqlCommand.Parameters.AddWithValue("@id_cita", 0);
                    sqlCommand.Parameters.AddWithValue("@id_paciente", citaDto.Id_paciente);
                    sqlCommand.Parameters.AddWithValue("@fecha_cita", citaDto.Fecha_cita);
                    sqlCommand.Parameters.AddWithValue("@hora_inicio", citaDto.Hora_inicio);
                    sqlCommand.Parameters.AddWithValue("@hora_fin", citaDto.Hora_fin);
                    sqlCommand.Parameters.AddWithValue("@estado_cita", citaDto.Estado_cita);
                    connect.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                return false;
            }
            return true;
        }

        /**
        * Este metodo actualiza los campos de la tabla citas
        * @return true si la sentencia se ejecuto de manera correcta
        * @return false si hubo algun error en la ejecucion de la sentencia preparada
        */
        public bool ActualizarCita(CitaDto citaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_citas", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 2);
                    sqlCommand.Parameters.AddWithValue("@id_cita", citaDto.Id_citas);
                    sqlCommand.Parameters.AddWithValue("@id_paciente", citaDto.Id_paciente);
                    sqlCommand.Parameters.AddWithValue("@fecha_cita", citaDto.Fecha_cita);
                    sqlCommand.Parameters.AddWithValue("@hora_inicio", citaDto.Hora_inicio);
                    sqlCommand.Parameters.AddWithValue("@hora_fin", citaDto.Hora_fin);
                    sqlCommand.Parameters.AddWithValue("@estado_cita", citaDto.Estado_cita);
                    connect.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                return false;
            }

            return true;
        }

        /*
         * Este metodo Elimina paciente por medio del atributo de la identificacion
         * @return false si hubo un error al eliminar al paciente
         * @return true si se borro de manera exito al paciente
         */
        public bool EliminarCita(int id)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("EliminarCita", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                 
                    sqlCommand.Parameters.AddWithValue("@id_cita", id);

                    connect.Open(); //abre la conexion hacia la base de datos

                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                return false;
            }
            return true;
        }


        /*
        * Este metodo consulta el paciente pasando como parametro la identificacion
        * @return pacienteDto devuelve el resultado del QUERY 
        * @param identificacion parametro de la identificacion que se va a consultar
        */
        public CitaDto ConsultarCita(string id_paciente)
        {
            CitaDto citaDto = new CitaDto();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("");
                    connect.Open();
                    sqlCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                citaDto = null;
            }
            return citaDto;
        }

        /*
         * Metodo para listar las citas que se encuentran registradas
         */
        public List<CitaDto> List()
        {
            DataSet dataSet = new DataSet();
            List<CitaDto> lista = new List<CitaDto>();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {

                    //SqlCommand sqlCommand = new SqlCommand("SELECT * FROM citas", connect);
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.ExecuteNonQuery();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("ListarCita", connect);
                    sqlDataAdapter.Fill(dataSet, "0");

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        var dtoCita = new CitaDto();

                        //dtoCita.Id_citas = Convert.ToInt32(row["id_cita"]);
                        dtoCita.Id_paciente = Convert.ToInt32(row["id_paciente"]);
                        dtoCita.Fecha_cita = Convert.ToDateTime(row["fecha_cita"]);
                        dtoCita.Hora_inicio = row["hora_inicio"].ToString();
                        dtoCita.Hora_fin = row["hora_fin"].ToString();
                        dtoCita.Estado_cita = Convert.ToInt32(row["estado_cita"]);
                        lista.Add(dtoCita);
                    }
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                lista = new List<CitaDto>();
            }
            return lista;
        }


    }
}
