using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Examen
    {
        public Examen()
        {
            ParticipacionExamen = new HashSet<ParticipacionExamen>();
            Pregunta = new HashSet<Pregunta>();
        }

        public int Examen1 { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFinal { get; set; }
        public int Curso { get; set; }

        public Curso CursoNavigation { get; set; }
        public ICollection<ParticipacionExamen> ParticipacionExamen { get; set; }
        public ICollection<Pregunta> Pregunta { get; set; }
    }
}
