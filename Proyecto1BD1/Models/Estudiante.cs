using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            CursoEstudiante = new HashSet<CursoEstudiante>();
            EstuResp = new HashSet<EstuResp>();
        }

        public int Carnet { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string TelefonoTutor { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NumeroPartida { get; set; }
        public string Fotografia { get; set; }
        public byte[] Password { get; set; }

        public ICollection<CursoEstudiante> CursoEstudiante { get; set; }
        public ICollection<EstuResp> EstuResp { get; set; }
    }
}
