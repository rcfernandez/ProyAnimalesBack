using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProyectoAnimales.Models.EF;

namespace ProyectoAnimales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            this.LimpiarTokens();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        // PARA LIMPIAR LOS TOKENS DE LOS USUARIOS QUE YA LES EXPIRO LA FECHA //
        private void LimpiarTokens()
        {
            try
            {
                using (AnimalesContext db = new AnimalesContext())
                {
                    var list = (from u in db.Usuario
                                where u.FechaExpiracion < DateTime.Now
                                select u).ToList();

                    if (list.Count > 0)
                    {
                        foreach (var usu in list)
                        {
                            var usuario = db.Usuario.Find(usu.Id);
                            usuario.Token = null;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            


        }


    }
}
