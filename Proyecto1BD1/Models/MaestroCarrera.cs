using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class MaestroCarrera
    {
        public MaestroCarrera()
        {
            Curso = new HashSet<Curso>();
        }

        public int Registro { get; set; }
        public int Carrera { get; set; }

        public Carrera CarreraNavigation { get; set; }
        public Maestro RegistroNavigation { get; set; }
        public ICollection<Curso> Curso { get; set; }
    }
}
