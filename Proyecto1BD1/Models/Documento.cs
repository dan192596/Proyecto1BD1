using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Documento
    {
        public int Documento1 { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Registro { get; set; }
        public int Carrera { get; set; }
        public int Curso { get; set; }
        public int Publicacion { get; set; }

        public MaestroCarrera MaestroCarrera { get; set; }
        public Publicacion PublicacionNavigation { get; set; }
    }
}
