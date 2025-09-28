using CRUD_PatronMVC.Data;
using CRUD_PatronMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PatronMVC.Controllers
{
    public class AuthController
    {
        private ClaseCRUD crud = new ClaseCRUD();

        public bool Autenticar(string usuario, string password)
        {
            try
            {
                string passwordHash = GenerarHashSHA256(password);
                string condicion = $"Usuario = '{usuario}' AND PasswordHash = '{passwordHash}' AND Activo = 1";

                DataTable dt = crud.ConsultarCondicion("u.Id, u.Usuario, u.IdRol, r.Nombre as RolNombre, u.Nombre, u.Apellido",
                    "Usuarios u INNER JOIN Roles r ON u.IdRol = r.Id", condicion);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Sesion.UsuarioId = Convert.ToInt32(row["Id"]);
                    Sesion.Username = row["Usuario"].ToString();
                    Sesion.RolId = Convert.ToInt32(row["IdRol"]);
                    Sesion.RolNombre = row["RolNombre"].ToString();
                    Sesion.NombreCompleto = $"{row["Nombre"]} {row["Apellido"]}";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en autenticación: {ex.Message}");
            }
        }

        public static string GenerarHashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
