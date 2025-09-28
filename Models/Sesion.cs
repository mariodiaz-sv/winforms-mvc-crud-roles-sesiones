using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PatronMVC.Models
{
    public static class Sesion
    {
        public static int UsuarioId { get; set; }
        public static string Username { get; set; }
        public static int RolId { get; set; }
        public static string RolNombre { get; set; }
        public static string NombreCompleto { get; set; }

        public static void Limpiar()
        {
            UsuarioId = 0;
            Username = string.Empty;
            RolId = 0;
            RolNombre = string.Empty;
            NombreCompleto = string.Empty;
        }
    }
}
