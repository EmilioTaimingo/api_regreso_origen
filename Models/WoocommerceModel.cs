using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api_regreso_origen.Models
{
    public class GuiaWoocommerceModel
    {
        public int TipoGuia { get; set; }
        public string GuiAIdentificador
        {
            get; set;
        }

        public class WoocommerceModel
        {
            public int id { get; set; }
            public string Fecha_Creacion { get; set; }
            public string Orden_Key { get; set; }
            public DatosFacturacion DatosEnvio { get; set; }
            public string Destinatario { get; set; }
            public string Direccion { get; set; }
            public string Celular { get; set; }
            public string Descripcion { get; set; }
            //informacion para generar guia
            public string Nombre_Destinatario { get; set; }
            public string ApellidoP { get; set; }
            public string ApellidoM { get; set; }
            public string Email { get; set; }
            public string CodigoPostal { get; set; }
            public string StoreHttp { get; set; }
            public string ConsumerKey { get; set; }
            public string ConsumerSecret { get; set; }
            public int WooId { get; set; }
            public string NombreTienda { get; set; }
            public Producto[] listaProductos { get; set; }
        }
        public class Estatus
        {
            public string status { get; set; }
        }

        public class Nota
        {
            public string note { get; set; }
        }

        public class DatosPedido
        {
            public int id { get; set; }
            public int parent_id { get; set; }
            public string status { get; set; }
            public string currency { get; set; }
            public string version { get; set; }
            public bool prices_include_tax { get; set; }
            public string date_created { get; set; }
            public string date_modified { get; set; }
            public string discount_total { get; set; }
            public string discount_tax { get; set; }
            public string shipping_total { get; set; }
            public string shipping_tax { get; set; }
            public string cart_tax { get; set; }
            public string total { get; set; }
            public string total_tax { get; set; }
            public int customer_id { get; set; }
            public string order_key { get; set; }
            public DatosFacturacion billing { get; set; }
            public DatosFacturacion shipping { get; set; }
            public string payment_method { get; set; }
            public string payment_method_title { get; set; }
            public string transaction_id { get; set; }
            public string customer_ip_address { get; set; }
            public string customer_user_agent { get; set; }
            public string created_via { get; set; }
            public string customer_note { get; set; }
            public string date_completed { get; set; }
            public string date_paid { get; set; }
            public string cart_hash { get; set; }
            public string number { get; set; }
            public Meta_Data[] meta_Datas { get; set; }
            public Producto[] line_items { get; set; }

        }
        public class DatosFacturacion
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address_1 { get; set; }
            public string address_2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }
            public string email { get; set; }
            public string phone { get; set; }

        }
        public class Meta_Data
        {
            public int id { get; set; }
            public string key { get; set; }
            public string value { get; set; }
        }



        public class Producto
        {
            public int id { get; set; }
            public string name { get; set; }
            public int product_id { get; set; }
            public int variation_id { get; set; }
            public int quantity { get; set; }
            public string tax_class { get; set; }
            public string subtotal { get; set; }
            public string subtotal_tax { get; set; }
            public string total { get; set; }
            public string total_tax { get; set; }
            public string sku { get; set; } = "N/A";
            public int price { get; set; }
            public Picture image { get; set; }
        }

        public class Picture
        {
            public string id { get; set; }
            public string src { get; set; }
        }
        public class Tax
        {
            public int id { get; set; }
            public string total { get; set; }
            public string subtotal { get; set; }
        }
        public class TiendaWooCommerceModel
        {
            public int ID_Temporal { get; set; }
            public int Woo_Id { get; set; }

            [Display(Name = "Razón Social")]
            public string RazonSocial { get; set; }

            [Display(Name = "Url de la tienda")]
            public string URL { get; set; }

            [Display(Name = "Consumer Key")]
            public string ConsumerKey { get; set; }

            [Display(Name = "Consumer Secret")]
            public string ConsumerSecret { get; set; }
            public DateTime FechaAlta { get; set; }

            [Display(Name = "Fecha de Registro")]
            public string FechaAlatString { get; set; }

            [Display(Name = "Nombre de Tienda")]
            public string NombreTienda { get; set; }
            [Display(Name = "Razon Social")]
            public int ClienteID { get; set; }
            public string FechaBaja { get; set; }

        }
    }
}