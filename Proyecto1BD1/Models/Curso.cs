using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Curso
    {
        public Curso()
        {
            MaestroCarrera = new HashSet<MaestroCarrera>();
        }

        public int Curso1 { get; set; }
        public string Nombre { get; set; }

        public ICollection<MaestroCarrera> MaestroCarrera { get; set; }
    }
}
