using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            MaestroCarrera = new HashSet<MaestroCarrera>();
        }

        public int Carrera1 { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<MaestroCarrera> MaestroCarrera { get; set; }
    }
}
