using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace PruebaTecnica.Core
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuentum>();
        }

        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }

        [JsonIgnore]
        public virtual Persona Persona { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cuentum> Cuenta { get; set; }
    }
}
