using System;
using System.Collections.Generic;

namespace ProyectoAnimales.Models.EF
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int? IdEstado { get; set; }
        public string Token { get; set; }
        public DateTime? FechaExpiracion { get; set; }
    }
}
