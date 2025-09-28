using CRUD_PatronMVC.Data;
using CRUD_PatronMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PatronMVC.Controllers
{
    public class UsuarioController
    {
        private ClaseCRUD crud = new ClaseCRUD();

        public DataTable ObtenerTodosUsuarios()
        {
            try
            {
                string query = @"SELECT u.Id, u.Usuario, r.Nombre as Rol, u.Nombre, u.Apellido, 
                                u.Sexo, u.Telefono, u.Correo, u.Activo
                                FROM Usuarios u INNER JOIN Roles r ON u.IdRol = r.Id
                                WHERE u.Activo = 1";
                crud.ConsultarGral(query, "Usuarios");
                return crud.ds.Tables["Usuarios"];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios: {ex.Message}");
            }
        }

        public bool CrearUsuario(Usuario usuario)
        {
            try
            {
                string campos = "Usuario, PasswordHash, IdRol, Nombre, Apellido, Sexo, Telefono, Correo";
                string valores = $"'{usuario.Username}', '{usuario.PasswordHash}', {usuario.IdRol}, " +
                                $"'{usuario.Nombre}', '{usuario.Apellido}', '{usuario.Sexo}', " +
                                $"'{usuario.Telefono}', '{usuario.Correo}'";

                return crud.Insertar("Usuarios", campos, valores);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}");
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                string campos = $"Usuario='{usuario.Username}', IdRol={usuario.IdRol}, " +
                               $"Nombre='{usuario.Nombre}', Apellido='{usuario.Apellido}', " +
                               $"Sexo='{usuario.Sexo}', Telefono='{usuario.Telefono}', " +
                               $"Correo='{usuario.Correo}'";
                string condicion = $"Id = {usuario.Id}";

                return crud.Actualizar("Usuarios", campos, condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar usuario: {ex.Message}");
            }
        }

       

        public DataTable ObtenerRoles()
        {
            try
            {
                return crud.Consultar("Id, Nombre", "Roles");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener roles: {ex.Message}");
            }
        }
        // En UsuarioController.cs - agrega este método
        public DataTable ObtenerUsuariosPorCondicion(string condicion)
        {
            try
            {
                string query = $"SELECT Id, Usuario FROM Usuarios WHERE {condicion}";
                return crud.ConsultarCondicion("Id, Usuario", "Usuarios", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios por condición: {ex.Message}");
            }
        }
       
        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                // ✅ ELIMINACIÓN LÓGICA (recomendado) - Cambiar Activo = 0
                string campos = "Activo = 0";
                string condicion = $"Id = {idUsuario}";

                return crud.Actualizar("Usuarios", campos, condicion);

                // ❌ O para eliminación física (NO recomendado):
                // string condicion = $"Id = {idUsuario}";
                // return crud.Eliminar("Usuarios", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar usuario: {ex.Message}");
            }
        }

        public DataTable ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                string condicion = $"Id = {idUsuario} AND Activo = 1";
                return crud.ConsultarCondicion("*", "Usuarios", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuario por ID: {ex.Message}");
            }
        }
        // En UsuarioController.cs
        public DataTable BuscarUsuariosPorNombreUsuario(string filtro)
        {
            try
            {
                // Escapar comillas simples para evitar inyección SQL
                string filtroSeguro = filtro.Replace("'", "''");

                // Construir condición (solo usuarios activos)
                string condicion = $"LOWER(Usuario) LIKE '%{filtroSeguro.ToLower()}%' AND Activo = 1";

                // Seleccionar las columnas que quieres mostrar
                string columnas = "Id, Usuario, Nombre, Apellido, Sexo, Telefono, Correo, IdRol";

                return crud.ConsultarCondicion(columnas, "Usuarios", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la búsqueda: {ex.Message}");
            }
        }
    }
}
