using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace PruebaTecnica.Core
{
    public partial class Persona
    {
        public int PersonaId { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public decimal Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
