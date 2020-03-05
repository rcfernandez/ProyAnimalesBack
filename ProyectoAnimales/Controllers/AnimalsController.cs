using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAnimales.Models;
using ProyectoAnimales.Models.EF;
using System.Net;
using ProyAnimalesBack.Models;

namespace ProyectoAnimales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[ValidateToken]
    [ValidateModel]
    public class AnimalsController : BaseController
    {
        private readonly AnimalesContext db = new AnimalesContext();

        // API/ANIMALS/GET
        [HttpGet]
        public Reply Get()
        {
            Reply oR = new Reply();

            var list = (from anim in db.Animalito
                        where anim.IdEstado.Equals(1)
                        select new
                        {
                            id = anim.Id,
                            nombre = anim.Nombre,
                            patas = anim.Patas

                        }).ToList();

            if(list.Count > 0)
            {
                oR.Result = 1;
                oR.Count = list.Count();
                oR.Message = "Se encontraron datos";
                oR.Data = list;
            }
            else
            {
                oR.Result = 2;
                oR.Message = "No se encontraron datos";
            }

            return oR;

        }

        // API/ANIMALS/ADD
        [HttpPost]
        public Reply Add(AnimalitoViewModel model)
        {
            
            Reply oR = new Reply();

            var list = (from a in db.Animalito
                        where a.Nombre == model.Nombre
                        select a).ToList();

            if (list.Count() > 0)
            {
                oR.Result = 2;
                oR.Message = "El animalito ya existe";
                return oR;
            }

            Animalito ani = new Animalito();
            ani.Nombre = model.Nombre;
            ani.Patas = model.Patas;
            ani.IdEstado = model.IdEstado;

            db.Animalito.Add(ani);
            db.SaveChanges();

            oR.Result = 1;
            oR.Message = "Se ha guardado correctamente";
            oR.Count = 1;
            oR.Data = ani;

            return oR;

        }


        // API/ANIMALS/EDIT
        [ValidateModel]
        [HttpPost]
        public Reply Edit(Animalito model)
        {
            Reply oR = new Reply();

            if (db.Animalito.Where(a => a.Nombre == model.Nombre).Count() > 0)
            {
                oR.Result = 2;
                oR.Count = 1;
                oR.Message = "Ya existe un animal con ese nombre";

                return oR;
            }

            Animalito ani = db.Animalito.Find(model.Id);

            ani.Nombre = model.Nombre;
            ani.Patas = model.Patas;
            ani.IdEstado = model.IdEstado;

            db.Entry(ani).State = EntityState.Modified;
            db.SaveChanges();

            oR.Result = 1;
            oR.Message = "Se ha editado correctamente";
            oR.Count = 1;
            oR.Data = model;

            return oR;

        }

        // API/ANIMALS/DELETE/id
        [HttpDelete("{id}")]
        public Reply Delete(int id)
        {

            Reply oR = new Reply();

            Animalito ani = db.Animalito.Find(id);

            if (ani != null)
            {
                if (ani.IdEstado == 0)      // IdEstado 0 = Eliminado
                {
                    oR.Result = 2;
                    oR.Message = "Ese animalito ya fue eliminado ¬¬";
                }

                if (ani.IdEstado == 1)      // IdEstado 1 = Activo
                {
                    ani.IdEstado = 0;
                    db.Entry(ani).State = EntityState.Modified;
                    db.SaveChanges();

                    oR.Data = ani;
                    oR.Message = "Se ha eliminado correctamente :)";
                    oR.Result = 1;
                    oR.Count = 1;
                }
            }
            else
            {
                oR.Result = 2;
                oR.Message = "No existe ese animalito, animalito! >:(";
            }


            return oR;

        }


        // API/ANIMALS/BUSCAR
        [HttpGet("{nombre}")]
        public Reply Search(string nombre)
        {
            Reply oR = new Reply();

            var list = (from anim in db.Animalito
                        where anim.IdEstado.Equals(1) && anim.Nombre == nombre
                        select new
                        {
                            id = anim.Id,
                            nombre = anim.Nombre,
                            patas = anim.Patas

                        }).ToList();

            if (list.Count > 0)
            {
                oR.Result = 1;
                oR.Count = list.Count();
                oR.Message = "Se ha encontrado el animal";
                oR.Data = list;
            }
            else
            {
                oR.Result = 2;
                oR.Message = "El Animal no se encuentra en la BD";
            }

            return oR;

        }


        // API/ANIMALS/EXISTE
        [HttpGet("{nombre}")]
        public Boolean Exist(string nombre)
        {

            var list = (from anim in db.Animalito
                        where anim.IdEstado.Equals(1) && anim.Nombre == nombre
                        select anim)
                        .ToList();

            if (list.Count > 0)
            {
                return true;
            }

            return false;

        }




    }
}