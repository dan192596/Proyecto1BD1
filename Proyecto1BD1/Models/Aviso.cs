using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Aviso
    {
        public int Aviso1 { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaHora { get; set; }
        public int TipoAviso { get; set; }
        public int Publicacion { get; set; }

        public Publicacion PublicacionNavigation { get; set; }
        public TipoAviso TipoAvisoNavigation { get; set; }
    }
}
