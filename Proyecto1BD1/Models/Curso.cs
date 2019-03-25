using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Curso
    {
        public Curso()
        {
            Actividad = new HashSet<Actividad>();
            CursoEstudiante = new HashSet<CursoEstudiante>();
            Documento = new HashSet<Documento>();
            Examen = new HashSet<Examen>();
        }

        public int Curso1 { get; set; }
        public string Nombre { get; set; }
        public int Registro { get; set; }
        public int Carrera { get; set; }

        public MaestroCarrera MaestroCarrera { get; set; }
        public ICollection<Actividad> Actividad { get; set; }
        public ICollection<CursoEstudiante> CursoEstudiante { get; set; }
        public ICollection<Documento> Documento { get; set; }
        public ICollection<Examen> Examen { get; set; }
    }
}
