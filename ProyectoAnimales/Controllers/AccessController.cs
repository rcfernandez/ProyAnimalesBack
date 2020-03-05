using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAnimales.Models;
using ProyectoAnimales.Models.EF;

namespace ProyectoAnimales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        public readonly AnimalesContext db = new AnimalesContext();

        // ACCESS/LOGIN
        [HttpPost]
        public Reply Login([FromBody] AccessViewModel model)
        {
            Reply oR = new Reply();
            oR.Result = 0;

            try
            {
                var listUsuario = db.Usuario.Where(u => u.Email == model.Email && u.Pass == model.Pass && u.IdEstado == 1);

                if (listUsuario.Count() > 0)
                {

                    var stringToken = Guid.NewGuid().ToString();
                    HttpContext.Response.Headers.Add("token", stringToken);
                          
                    Usuario oUsuario = listUsuario.First();
                    oUsuario.Token = stringToken;
                    // TIEMPO PARA QUE CADUQUE EL TOKEN = 12 horas
                    oUsuario.FechaExpiracion = DateTime.Now.AddHours(12);

                    db.Entry(oUsuario).State = EntityState.Modified;
                    db.SaveChanges();

                    oR.Count = 1;
                    oR.Message = "Se ha generado el token correctamente";
                    oR.Result = 1;
                }

                else
                {
                    oR.Message = "Usuario no valido";
                    oR.Result = 2;
                }

            }
            catch (Exception ex)
            {
                oR.Message = "Login fallo. " + ex.Message;
            }

            return oR;

        }




    }
}