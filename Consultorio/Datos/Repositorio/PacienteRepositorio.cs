using Datos.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Repositorio
{
    public class PacienteRepositorio
    {
        
        /*
         * Metodo para guardar un nuevo paciente en la base de datos
         * @return false devuelve false el cual quiere decir que no se hizo el registro correctamente
         * @return true devuelve verdadero donde el registro fue realizado correctamente
         * @param pacienteDto atributos de la clase PacienteDto
         */
        public bool GuardarPaciente(PacienteDto pacienteDto)
        {
            
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString.ToString()))
                {

                    SqlCommand sqlCommand = new SqlCommand("sp_paciente", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    
                    sqlCommand.Parameters.AddWithValue("@procedure", 1);
                    //sqlCommand.Parameters.AddWithValue("@id_paciente", 0);
                    sqlCommand.Parameters.AddWithValue("@identificacion", pacienteDto.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@nombre_completo", pacienteDto.Nombre_completo);
                    sqlCommand.Parameters.AddWithValue("@direccion", pacienteDto.Direccion);
                    sqlCommand.Parameters.AddWithValue("@telefono", pacienteDto.Telefono);
                    sqlCommand.Parameters.AddWithValue("@horario_contacto", pacienteDto.Horario_contacto);

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

        /**
         * Este metodo actualiza los campos de la tabla paciente
         * @return true si la sentencia se ejecuto de manera correcta
         * @return false si hubo algun error en la ejecucion de la sentencia preparada
         */
        public bool ActualizarPaciente(PacienteDto pacienteDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_paciente", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 2);
                    //sqlCommand.Parameters.AddWithValue("@id_paciente", 0);
                    sqlCommand.Parameters.AddWithValue("@identificacion", pacienteDto.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@nombre_completo", pacienteDto.Nombre_completo);
                    sqlCommand.Parameters.AddWithValue("@direccion", pacienteDto.Direccion);
                    sqlCommand.Parameters.AddWithValue("@telefono", pacienteDto.Telefono);
                    sqlCommand.Parameters.AddWithValue("@horario_contacto", pacienteDto.Horario_contacto);
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

        /*
         * Este metodo Elimina paciente por medio del atributo de la identificacion
         * @return false si hubo un error al eliminar al paciente
         * @return true si se borro de manera exito al paciente
         */
        public bool EliminarPaciente(string id)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("EliminarPaciente", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@identificacion", id);

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
        public PacienteDto ConsultarPaciente(string identificacion)
        {
            PacienteDto pacienteDto = new PacienteDto();

            try
            {
                using ( SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("ConsultarPaciente");
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@identificacion", identificacion); // Parametro de identificacion
                    connect.Open(); // Abre la conexion
                    sqlCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                pacienteDto = null;
            }
            return pacienteDto;
        }

        /*
         * Este metodo se encarga de listar todos los pacientes que se encuentras registrados en la base de 
         * datos
         */
        public List<PacienteDto> listarPaciente()
        {
            
            List<PacienteDto> lista = new List<PacienteDto>();

            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {

                    //SqlCommand sqlCommand = new SqlCommand("SELECT identificacion,nombre_completo, direccion, telefono, horario_contacto FROM Pacientes",connect);
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.ExecuteNonQuery();
                    
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("ListarPaciente", connect);
                   
                    sqlDataAdapter.Fill(dataSet, "0");
                                        
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        var dtoPaciente = new PacienteDto();

                        //dtoPaciente.Id_paciente = Convert.ToInt32(row["id_paciente"]);
                        dtoPaciente.Identificacion = row["identificacion"].ToString();
                        dtoPaciente.Nombre_completo = row["nombre_completo"].ToString();
                        dtoPaciente.Direccion = row["direccion"].ToString();
                        dtoPaciente.Telefono = row["telefono"].ToString();
                        dtoPaciente.Horario_contacto = Convert.ToInt32(row["horario_contacto"]);
                        lista.Add(dtoPaciente);
                        
                    }
                }
            }
            catch(Exception ex)
            {
                string message_err = ex.Message;
                lista  = new List<PacienteDto>(); 
            }
            return lista;
        }


    }
}
