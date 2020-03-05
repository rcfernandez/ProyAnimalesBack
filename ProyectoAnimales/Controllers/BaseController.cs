using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAnimales.Models.EF;

namespace ProyectoAnimales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public bool Verify(string token)
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