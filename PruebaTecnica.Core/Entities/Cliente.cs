using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaTecnica.Core
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuentum>();
        }

        public int PersonaId { get; set; }
        public int ClienteId { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual ICollection<Cuentum> Cuenta { get; set; }
    }
}
