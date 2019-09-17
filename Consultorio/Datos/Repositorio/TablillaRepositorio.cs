using Datos.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Repositorio
{
    /*
     * Clase CRUD de la tabla Tablilla Precio
     */
    public class TablillaRepositorio
    {

        /*
         * Metodoque se encarga de insertar tablillas en la base de datos
         * @param tablillaDto se pasa por parametro los atributos de la tabla Tablilla por medio de Set
         */
        public bool guardarTablilla(TablillaDto tablillaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_tablilla", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;   
                    sqlCommand.Parameters.AddWithValue("@procedure", 1);
                    sqlCommand.Parameters.AddWithValue("@id_tablilla", 0);
                    sqlCommand.Parameters.AddWithValue("@nombre", tablillaDto.Nombre);
                    sqlCommand.Parameters.AddWithValue("@precio", tablillaDto.Precio);
                    
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
         * Metodo que se encarga de actualizar los campos de la tablilla 
         * @param tablillDto pasa por parametro los atrbutos de la tabla tablilla
         */
        public bool actualizarTablilla(TablillaDto tablillaDto)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_tablilla", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@procedure", 2);
                    sqlCommand.Parameters.AddWithValue("@id_tablilla", tablillaDto.Id_tablilla);
                    sqlCommand.Parameters.AddWithValue("@nombre", tablillaDto.Nombre);
                    sqlCommand.Parameters.AddWithValue("@precio", tablillaDto.Precio);
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
         * Metodo que se encarga de eliminar la tablilla por medio del Id
         * @param id recibe el id que se va a eliminar
         * @return false
         * @return true
         */
        public bool eliminarTablilla(int id)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("EliminarTablilla", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    
                    sqlCommand.Parameters.AddWithValue("@id_tablilla", id);
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
         * Metodo que se encarga de listar las tablillas que se encuentras registradas en la base de datos
         * 
         */
        public List<TablillaDto> listarTablilla()
        {
            DataSet dataSet = new DataSet();
            List<TablillaDto> lista = new List<TablillaDto>();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString))
                {

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("ListarTablilla", connect);
                    
                    sqlDataAdapter.Fill(dataSet, "0");

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        var dtoTablilla = new TablillaDto();

                        dtoTablilla.Id_tablilla = Convert.ToInt32(row["id_tablilla"]);    
                        dtoTablilla.Nombre = row["nombre"].ToString();
                        dtoTablilla.Precio = Convert.ToInt32(row["precio"]);
                   
                        lista.Add(dtoTablilla);
                    }
                }
            }
            catch (Exception ex)
            {
                string message_err = ex.Message;
                lista = new List<TablillaDto>();
            }
            return lista;
        }
    }
}
