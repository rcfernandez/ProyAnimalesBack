using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAnimales.Models.EF
{
    public class AnimalitoViewModel
    {

        [Required(ErrorMessage ="Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Patas es requerido")]
        public int? Patas { get; set; }

        [Required(ErrorMessage = "IdEstado es requerido")]
        public int? IdEstado { get; set; }

    }
}
