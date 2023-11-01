using api_regreso_origen.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace api_regreso_origen.Context
{
    public class ValidaGuia : DBContext
    {
        public CamposGuiaTaimingo Valida_Guia(string Guia)//Llamado los detalles de la atrea
        {
            CamposGuiaTaimingo oDatosGuia = new CamposGuiaTaimingo();
            string connectionString = $"server ={GetRDSConections().Writer}; {Data_base}";

            //---------------------------------------------------------------------
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("get_cliente_from_guia_sp", conexion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("paqguia", Guia);

                try
                {
                    conexion.Open();
                    MySqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oDatosGuia.Cliente_Id = Convert.ToInt32(dr["cli_id"].ToString());
                        oDatosGuia.RazonSocial = dr["cli_razonsocial"].ToString();
                        oDatosGuia.Tipo_Guia = dr["tgu_id"].ToString() is "" ? 0 : Convert.ToInt32(dr["tgu_id"].ToString());
                        oDatosGuia.Identificador = dr["gui_identificador"].ToString();

                    }
                    conexion.Close();//cierra conexion
                    dr.Close();//cierra lista
                    return oDatosGuia;
                }

                catch (Exception ex)
                {

                    return oDatosGuia;
                }

            }
        }

        //cambiamos el estatus en la base de datos estatus regreso a origen
        public void RegresoAOrigen(DataGuideComment datosGuia)
        {
            //Conexión a la base de datos //Writer porque Altas son escrituras
            string connectionString = $"server ={GetRDSConections().Writer}; {Data_base}";
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                //Comandos Generales Alta
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "regreso_origen_paquete_sp";
                cmd.Parameters.AddWithValue("paqguia", datosGuia.Guide);
                cmd.Parameters.AddWithValue("tarcomentarios", datosGuia.Comments);
                cmd.Parameters.AddWithValue("tarid", datosGuia.IDTask);
                cmd.Parameters.AddWithValue("tarusu", datosGuia.IDUser);
                cmd.Parameters.AddWithValue("tarlatitud", datosGuia.Latitude);
                cmd.Parameters.AddWithValue("tarlongitud", datosGuia.Longitude);
                cmd.Parameters.AddWithValue("tarevidencia", datosGuia.Evidence);
               
                // Cierre General
                conexion.Open();
                int res = cmd.ExecuteNonQuery();
                conexion.Close();
                cmd = null;
            }
        }


    }
}