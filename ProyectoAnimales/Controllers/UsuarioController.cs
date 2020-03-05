using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAnimales.Models;
using ProyectoAnimales.Models.EF;

namespace ProyectoAnimales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public Reply Get()
        {
            Reply oR = new Reply();
            oR.Result = 0;
            try
            {
                using (AnimalesContext db = new AnimalesContext())
                {
                    var list = (from usu in db.Usuario
                                where usu.IdEstado.Equals(1)
                                select new
                                {
                                    id = usu.Id,
                                    usuario = usu.Usuario1,
                                    email = usu.Email,
                                    pass = usu.Pass,
                                    token = usu.Token,
                                    idEstado = usu.IdEstado

                                }).ToList();

                    if (list.Count > 0)
                    {
                        oR.Data = list;
                        oR.Count = list.Count();
                        oR.Message = "Se encontraron datos";
                        oR.Result = 1;
                    }
                    else
                    {
                        oR.Message = "No se encontraron datos";
                    }
                }

            }
            catch (Exception ex)
            {
                oR.Message = "Ha ocurrido un error al traer el listado " + ex.Message;
            }

            return oR;

        }


        [HttpPost]
        public Reply Add([FromBody]Usuario model)
        {
            Reply oR = new Reply();
            oR.Result = 0;

            try
            {
                Usuario usu = new Usuario();
                usu.Usuario1 = model.Usuario1;
                usu.Email = model.Email;
                usu.Pass = model.Pass;
                usu.IdEstado = model.IdEstado;

                using (AnimalesContext db = new AnimalesContext())
                {
                    db.Usuario.Add(usu);
                    db.SaveChanges();
                }

                oR.Data = usu;
                oR.Message = "Se ha guardado correctamente";
                oR.Result = 1;
                oR.Count = 1;

            }
            catch (Exception ex)
            {
                oR.Message = "Se ha producido un error al guardar. " + ex.Message;
            }

            return oR;

        }




    }
}