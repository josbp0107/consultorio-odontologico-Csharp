using Datos.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Datos.Repositorio
{
    public class HistoriaClinicaRepositorio
    {
        /*
         * Este metodo se encarga de guardar las historias clinicas
         * @param historiaClinicaDto apunta al metodo de HistoriaClinicaDto donde se encuentra los atributos de la tabla HistoriaClinica
         * @return false Si se encuentra un error al momento de insertar 
         * @return true fue existoso la inserción 
         */
        public bool GuardarHistoriaClinica(HistoriaClinicaDto historiaClinicaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_historia_clinica", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 1);
                    sqlCommand.Parameters.AddWithValue("@id_paciente", historiaClinicaDto.Id_paciente);
                    sqlCommand.Parameters.AddWithValue("@procedimiento_hecho", historiaClinicaDto.Procedimiento_hecho);
                    sqlCommand.Parameters.AddWithValue("@fecha_realizacion", historiaClinicaDto.Fecha_realizacion);
                    sqlCommand.Parameters.AddWithValue("@descripcion", historiaClinicaDto.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@id_tablilla", historiaClinicaDto.Id_tablilla);
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
         * Metodo que se encarga de actualizar las historias clinicas
         */
        public bool ActualizarHistoriaClinica(HistoriaClinicaDto historiaClinicaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_historia_clinica", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 2);
                    sqlCommand.Parameters.AddWithValue("@id_historia", 0);
                    sqlCommand.Parameters.AddWithValue("@id_paciente", historiaClinicaDto.Id_paciente);
                    sqlCommand.Parameters.AddWithValue("@procedimiento_hecho", historiaClinicaDto.Procedimiento_hecho);
                    sqlCommand.Parameters.AddWithValue("@fecha_realizacion", historiaClinicaDto.Fecha_realizacion);
                    sqlCommand.Parameters.AddWithValue("@descripcion", historiaClinicaDto.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@id_tablilla", historiaClinicaDto.Id_tablilla);
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


        public bool EliminarHistorial(int id)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("EliminarHistoriaClinica", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@id_historia", id);
                    connect.Open(); //abre la conexion hacia la base de datos
                    sqlCommand.ExecuteNonQuery(); //Ejecuta la sentencia preparada
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                return false;
            }
            return true;
        }


        public CitaDto ConsultarHistorialClinica(string id_paciente)
        {
            CitaDto citaDto = new CitaDto();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM historias_clinicas WHERE id_paciente =" + id_paciente);
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
        public List<HistoriaClinicaDto> listarHistorias()
        {
            DataSet dataSet = new DataSet();
            List<HistoriaClinicaDto> lista = new List<HistoriaClinicaDto>();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {

                    //SqlCommand sqlCommand = new SqlCommand("SELECT * FROM historias_clinicas", connect);
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.ExecuteNonQuery();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("listarHistorial", connect);
                    sqlDataAdapter.Fill(dataSet, "0");

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        var dtoHistoriaClinica = new HistoriaClinicaDto();
                        //dtoHistoriaClinica.Id_historia = Convert.ToInt32(row["id_historia"]);
                        dtoHistoriaClinica.Id_paciente = Convert.ToInt32(row["id_paciente"]);
                        dtoHistoriaClinica.Procedimiento_hecho = row["procedimiento_hecho"].ToString();
                        dtoHistoriaClinica.Fecha_realizacion = Convert.ToDateTime(row["fecha_realizacion"]);
                        dtoHistoriaClinica.Id_tablilla = Convert.ToInt32(row["id_tablilla"]);
                        lista.Add(dtoHistoriaClinica);
                    }
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                lista = new List<HistoriaClinicaDto>();
            }
            return lista;
        }


    }
}
