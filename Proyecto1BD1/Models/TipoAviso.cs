using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class TipoAviso
    {
        public TipoAviso()
        {
            Aviso = new HashSet<Aviso>();
        }

        public int TipoAviso1 { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Aviso> Aviso { get; set; }
    }
}
