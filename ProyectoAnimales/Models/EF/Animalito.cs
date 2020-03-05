using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAnimales.Models.EF
{
    public partial class Animalito
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int? Patas { get; set; }
        public int? IdEstado { get; set; }
    }
}
