using System;
using System.Collections.Generic;

namespace Proyecto1BD1.Models
{
    public partial class Maestro
    {
        public Maestro()
        {
            MaestroCarrera = new HashSet<MaestroCarrera>();
        }

        public int Registro { get; set; }
        public int Dpi { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public byte[] Fotografia { get; set; }
        public byte[] Password { get; set; }

        public ICollection<MaestroCarrera> MaestroCarrera { get; set; }
    }
}
