using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            Respuesta = new HashSet<Respuesta>();
        }

        public int Pregunta1 { get; set; }
        public int Examen { get; set; }
        public string Descripcion { get; set; }

        public Examen ExamenNavigation { get; set; }
        public ICollection<Respuesta> Respuesta { get; set; }
    }
}
