using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto1BD1.Controllers
{
    public static class VariablesGlobales
    {
        public enum TipoUsuario{
            Maestro,
            Estudiante,
            Administrador
        }
        // read-write variable
        public static TipoUsuario Tipo { get; set; }//Si es maestro, estudiante o administrador
        public static string Codigo { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string Fotografia { get; set; }
    }
}
