﻿using api_regreso_origen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Web;

namespace api_regreso_origen.Context
{
    public class SendEstatusCommand
    {
        public ReplyLiverpool Cambia_Status(string Guia, string IdentifierGuide)
        {
            try {
                string url = "https://apigee-pro.liverpool.com.mx/liverpool/4pl/marketplaceExt/estatus\r\n";
                guiaImg guiaImg = new guiaImg
                {
                    guiaFirmaB64 = "",
                    guiaImagenB64 = "",
                    guiaNomReci = "",
                    guiaParentesco = "",
                    guiaPersona = ""
                };
                //pasamos los parametros que vamos a imprimir 
                DataGuide oPost = new DataGuide
                {
                    thirdPl = "048",
                    tn_reference = IdentifierGuide,
                    estimated_delivery_date = DateTime.Now.AddHours(5).ToString(),
                    tracking_number = Guia,
                    code = "REGRESO A ORIGEN",
                    commen = "Se regresa a origen",
                    date = DateTime.Now.ToString(),
                    id_solicitante = "MKPL",
                    new_coord = "",
                    motivo = "",
                    user_autoriza = "MKPL",
                    guiaImg = guiaImg

                };

                var data = JsonSerializer.Serialize<DataGuide>(oPost);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("apikey", "XKQxINLolY1vTqyAoVzPDHzRENq6eQNcaNCON4sABBNzalA6");
                    var httpResponse = client.PostAsync(url, content).Result;
                    var res = httpResponse.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<ReplyLiverpool>(res);
                }
            }
            catch(Exception e) 
            { 
                return null; 
            }
        }
    }
}