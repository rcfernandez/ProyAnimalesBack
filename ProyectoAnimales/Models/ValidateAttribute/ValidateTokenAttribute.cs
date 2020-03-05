using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProyectoAnimales.Models;
using ProyectoAnimales.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProyAnimalesBack.Models
{
    public class ValidateTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            // VALIDAR TOKEN

            Reply oR = new Reply();
            oR.Result = 0;
            oR.Count = 0;


            if (!Verify(context.HttpContext.Request.Headers["token"].ToString()))
            {
                oR.Message = "No tienes permiso, logueate nuevamente";
                context.Result = new ObjectResult(oR);
            }


        }



        private bool Verify(string token)
        {
            using (AnimalesContext db = new AnimalesContext())
            {
                if (db.Usuario.Where(u => u.Token == token && u.FechaExpiracion > DateTime.Now && u.IdEstado == 1).Count() > 0)
                {
                    return true;
                }

                return false;

            }

        }

    }
}
