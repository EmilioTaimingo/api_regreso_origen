using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_regreso_origen.Models
{
    public class CamposGuiaTaimingo
    {
        public int Cliente_Id { get; set; }
        public string RazonSocial { get; set; }
        public int Tipo_Guia { get; set; } = 0;
        public string Identificador { get; set; } = "";
    }
}