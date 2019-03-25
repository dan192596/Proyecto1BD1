using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Publicacion
    {
        public Publicacion()
        {
            Actividad = new HashSet<Actividad>();
            Aviso = new HashSet<Aviso>();
            Documento = new HashSet<Documento>();
        }

        public int Publicacion1 { get; set; }
        public string Descripcion { get; set; }
        public DateTime FecaHora { get; set; }

        public ICollection<Actividad> Actividad { get; set; }
        public ICollection<Aviso> Aviso { get; set; }
        public ICollection<Documento> Documento { get; set; }
    }
}
