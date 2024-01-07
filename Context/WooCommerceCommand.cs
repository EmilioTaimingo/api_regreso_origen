using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;
using api_regreso_origen.Models;
using static api_regreso_origen.Models.GuiaWoocommerceModel;


namespace api_regreso_origen.Context
{
    public class WooCommerceCommand : DBContext
    {

        public GuiaWoocommerceModel Muestra_GuiasMod(string guia)//muestra todos los registros activos
        {

            string connectionString = $"server ={GetRDSConections().Writer}; {Data_base}";
            List<GuiaWoocommerceModel> oList = new List<GuiaWoocommerceModel>();

            // Utiliza dispose al finalizar bloque
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // Comandos
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "muestra_guiasmod_sp";
                //parametros
                cmd.Parameters.AddWithValue("guiguia", guia);
                conexion.Open();
                var leer = cmd.ExecuteReader();

                while (leer.Read() && !leer.NextResult())
                {
                }

                while (leer.Read())
                {
                    GuiaWoocommerceModel oModel = new GuiaWoocommerceModel();
                    oModel.TipoGuia = Convert.ToInt32(leer["tgu_id"]);
                    oModel.GuiAIdentificador = leer["gui_identificador"].ToString();
                    oList.Add(oModel);
                }
                conexion.Close();//cierra conexion
                leer.Close();//cierra lista
                return oList.FirstOrDefault();//regresa la lista con datos
            }
        }

        public void VerificaGuiaWooCommerce(string guia, string mensaje)
        {
            var oDatosGuia = Muestra_GuiasMod(guia);
            if (oDatosGuia.TipoGuia == 7)
            {
                string[] id = oDatosGuia.GuiAIdentificador.Split('-');
                var _id = Convert.ToInt32(id[0]);
                var datosTienda = MuestraTiendaWooCommerce_mod(Convert.ToInt32(id[2]));
                var verificacionConexion = new WoocommerceModel
                {
                    StoreHttp = datosTienda.URL,
                    ConsumerSecret = datosTienda.ConsumerSecret,
                    ConsumerKey = datosTienda.ConsumerKey,
                };
                AgregaNota(mensaje, _id, verificacionConexion);
            }

        }



        public int AgregaNota(string mensaje, int id, WoocommerceModel datos)
        {
            var StoreHttp = datos.StoreHttp;
            var ConsumerKey = datos.ConsumerKey;
            var ConsumerSecret = datos.ConsumerSecret;
            var list = new List<WoocommerceModel>();
            var nota = new Nota();
            nota.note = mensaje;
            var client = new RestClient($"{StoreHttp}/wp-json/wc/v2/orders/{id}/notes")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, "", "")
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var data = System.Text.Json.JsonSerializer.Serialize(nota);
            // HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            request.AddJsonBody(data);

            var response = client.Execute(request);
            if (response != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public TiendaWooCommerceModel MuestraTiendaWooCommerce_mod(int id)//muestra todos los comdiciones activos
        {
            List<TiendaWooCommerceModel> List = new List<TiendaWooCommerceModel>();
            string connectionString = $"server ={GetRDSConections().Reader}; {Data_base}";

            // Utiliza dispose al finalizar bloque
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // Comandos
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "muestra_tiendas_woocommerce_sp";
                cmd.Parameters.AddWithValue("wooid", id);
                conexion.Open();
                var leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    List.Add(new TiendaWooCommerceModel()//llena la lista de datos
                    {
                        Woo_Id = leer.GetInt32("woo_id"),
                        URL = leer["woo_storetttp"].ToString(),
                        ConsumerKey = leer["woo_consumerkey"].ToString(),
                        ConsumerSecret = leer["woo_consumersecret"].ToString(),
                        FechaAlta = Convert.ToDateTime(leer["woo_fechaalta"]),
                        FechaAlatString = leer["woo_fechaalta"].ToString(),
                        NombreTienda = leer["woo_tienda"].ToString(),
                        RazonSocial = leer["cli_razonsocial"].ToString(),
                        ClienteID = leer.GetInt32("cli_id"),
                    }); ;
                }
                conexion.Close();//cierra conexion
                leer.Close();//cierra lista
                return List.FirstOrDefault();//regresa la lista con datos
            }
        }



    }
}