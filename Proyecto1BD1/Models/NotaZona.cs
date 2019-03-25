using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class NotaZona
    {
        public decimal? Nota { get; set; }
        public string Observacion { get; set; }
        public int Actividad { get; set; }
        public int Curso { get; set; }
        public int Carnet { get; set; }

        public Actividad ActividadNavigation { get; set; }
        public CursoEstudiante C { get; set; }
    }
}
