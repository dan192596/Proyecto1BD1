using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class ParticipacionExamen
    {
        public int Examen { get; set; }
        public decimal? Nota { get; set; }
        public int Carnet { get; set; }
        public int Registro { get; set; }
        public int Carrera { get; set; }
        public int Curso { get; set; }

        public CursoEstudiante CursoEstudiante { get; set; }
        public Examen ExamenNavigation { get; set; }
    }
}
