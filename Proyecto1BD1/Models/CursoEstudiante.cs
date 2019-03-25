using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class CursoEstudiante
    {
        public CursoEstudiante()
        {
            NotaZona = new HashSet<NotaZona>();
            ParticipacionExamen = new HashSet<ParticipacionExamen>();
        }

        public int Carnet { get; set; }
        public int Curso { get; set; }

        public Estudiante CarnetNavigation { get; set; }
        public Curso CursoNavigation { get; set; }
        public ICollection<NotaZona> NotaZona { get; set; }
        public ICollection<ParticipacionExamen> ParticipacionExamen { get; set; }
    }
}
