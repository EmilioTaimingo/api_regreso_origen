﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using api_regreso_origen.Models;
using System.Web.Http;
using api_regreso_origen.Context;

namespace api_regreso_origen.Controllers
{
    public class STATUS_RETURNED_TO_ORIGINController : ApiController
    {
        public Reply Post([FromBody] DataGuideComment odatos)//valida las credenciales de acceso(usuario,contraseña)
        {
            var respuesta = new Reply();
            ValidaGuia oValidaTipoGuia = new ValidaGuia();
            SendEstatusCommand oStatus = new SendEstatusCommand();
            var datos = oValidaTipoGuia.Valida_Guia(odatos.Guide);
            WooCommerceCommand oWooCommerce = new WooCommerceCommand();
            oWooCommerce.VerificaGuiaWooCommerce(odatos.Guide, "El paquete a sido devuelto a origen");
            if (datos.Tipo_Guia == 6)
            {
                //cambiamos el estatus 
                var respuestaLiverpool = oStatus.Cambia_Status(odatos.Guide, datos.Identificador);
                if (respuestaLiverpool.tipo_respuesta == "OK")
                {
                    respuesta.Result = 200;
                    respuesta.Message = respuestaLiverpool.respuesta;
                    respuesta.type_Reply_Liverpool = respuestaLiverpool.tipo_respuesta;
                }
                else
                {
                    respuesta.Result = 400;
                    respuesta.Message = respuestaLiverpool.respuesta;
                    respuesta.type_Reply_Liverpool = respuestaLiverpool.tipo_respuesta;
                }


            }
            else if (datos.Cliente_Id == 0)
            {
                respuesta.Message = "La guia ingresada no existe";
                respuesta.Result = 404;
            }
            else if (datos.Cliente_Id != 0)
            {
                respuesta.Message = "La guia ingresada no pertenece a Liverpool";
                respuesta.Result = 200;
            }
            //cambiamos el estatus del paquete
            oValidaTipoGuia.RegresoAOrigen(odatos);

            return respuesta;
        }
    }
}
