using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Respuesta
    {
        public Respuesta()
        {
            EstuResp = new HashSet<EstuResp>();
        }

        public int Respuesta1 { get; set; }
        public string Descripcion { get; set; }
        public int Pregunta { get; set; }
        public int Examen { get; set; }

        public Pregunta PreguntaNavigation { get; set; }
        public ICollection<EstuResp> EstuResp { get; set; }
    }
}
