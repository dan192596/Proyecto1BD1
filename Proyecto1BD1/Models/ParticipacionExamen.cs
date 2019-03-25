using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class ParticipacionExamen
    {
        public int Examen { get; set; }
        public decimal? Nota { get; set; }
        public int Curso { get; set; }
        public int Carnet { get; set; }

        public CursoEstudiante C { get; set; }
        public Examen ExamenNavigation { get; set; }
    }
}
