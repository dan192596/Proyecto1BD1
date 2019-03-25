using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class EstuResp
    {
        public int Respuesta { get; set; }
        public int Carnet { get; set; }
        public int Pregunta { get; set; }
        public int Examen { get; set; }

        public Estudiante CarnetNavigation { get; set; }
        public Respuesta RespuestaNavigation { get; set; }
    }
}
