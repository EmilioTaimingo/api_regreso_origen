using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_regreso_origen.Models
{
    public class DataGuideComment
    {
        public string Guide { get; set; }
        public int IDTask { get; set; }
        public int IDUser { get; set; }
        public string Comments { get; set; }
        public string Evidence { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }   
    }
}