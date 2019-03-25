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
        public int Registro { get; set; }
        public int Carrera { get; set; }
        public int Curso { get; set; }

        public MaestroCarrera MaestroCarrera { get; set; }
        public ICollection<ParticipacionExamen> ParticipacionExamen { get; set; }
        public ICollection<Pregunta> Pregunta { get; set; }
    }
}
