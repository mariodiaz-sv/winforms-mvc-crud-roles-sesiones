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
    public class RolController
    {
        private ClaseCRUD crud = new ClaseCRUD();

        public DataTable ObtenerTodosRoles()
        {
            try
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Roles";
                crud.ConsultarGral(query, "Roles");
                return crud.ds.Tables["Roles"];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener roles: {ex.Message}");
            }
        }

        public DataTable ObtenerRolesParaComboBox()
        {
            try
            {
                return crud.Consultar("Id, Nombre", "Roles");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener roles para combo: {ex.Message}");
            }
        }

        public string ObtenerNombreRol(int idRol)
        {
            try
            {
                string condicion = $"Id = {idRol}";
                DataTable dt = crud.ConsultarCondicion("Nombre", "Roles", condicion);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Nombre"].ToString();
                }
                return "Desconocido";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener nombre del rol: {ex.Message}");
            }
        }

        public bool CrearRol(Rol rol)
        {
            try
            {
                string campos = "Nombre, Descripcion";
                string valores = $"'{rol.Nombre}', '{rol.Descripcion}'";

                return crud.Insertar("Roles", campos, valores);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear rol: {ex.Message}");
            }
        }

        public bool ActualizarRol(Rol rol)
        {
            try
            {
                string campos = $"Nombre = '{rol.Nombre}', Descripcion = '{rol.Descripcion}'";
                string condicion = $"Id = {rol.Id}";

                return crud.Actualizar("Roles", campos, condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar rol: {ex.Message}");
            }
        }

        public bool EliminarRol(int idRol)
        {
            try
            {
                string condicion = $"Id = {idRol}";
                string campos = "Activo = 0"; // Eliminación lógica
                return crud.Actualizar("Roles", campos, condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar rol: {ex.Message}");
            }
        }
    }
}
