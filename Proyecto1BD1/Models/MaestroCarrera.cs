using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class MaestroCarrera
    {
        public MaestroCarrera()
        {
            Actividad = new HashSet<Actividad>();
            CursoEstudiante = new HashSet<CursoEstudiante>();
            Documento = new HashSet<Documento>();
            Examen = new HashSet<Examen>();
        }

        public int Registro { get; set; }
        public int Carrera { get; set; }
        public int Curso { get; set; }

        public Carrera CarreraNavigation { get; set; }
        public Curso CursoNavigation { get; set; }
        public Maestro RegistroNavigation { get; set; }
        public ICollection<Actividad> Actividad { get; set; }
        public ICollection<CursoEstudiante> CursoEstudiante { get; set; }
        public ICollection<Documento> Documento { get; set; }
        public ICollection<Examen> Examen { get; set; }
    }
}
