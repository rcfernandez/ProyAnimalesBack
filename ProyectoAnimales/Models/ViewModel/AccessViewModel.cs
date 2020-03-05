using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAnimales.Models
{
    public class AccessViewModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Pass { get; set; }

    }
}
