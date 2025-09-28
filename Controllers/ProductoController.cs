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
    public class ProductoController
    {
        private ClaseCRUD crud = new ClaseCRUD();

        public DataTable ObtenerTodosProductos()//BUSCAR
        {
            try
            {
                // ✅ MEJORADO: Usar ConsultarCondicion en lugar de ConsultarGral
                string condicion = "Activo = 1";
                string columnas = "Id, Nombre, Precio, Stock";
                return crud.ConsultarCondicion(columnas, "Productos", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos: {ex.Message}");
            }
        }
        public DataTable ObtenerProductosPorCondicion(string condicion)
        {
            try
            {
                // ✅ MEJORADO: Devolver todas las columnas necesarias
                string columnas = "Id, Nombre, Precio, Stock";
                return crud.ConsultarCondicion(columnas, "Productos", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos por condición: {ex.Message}");
            }
        }
        public DataTable BuscarProductosPorNombre(string filtro)
        {
            try
            {
                // Escapar comillas simples para evitar inyección SQL
                string filtroSeguro = filtro.Replace("'", "''");
                string condicion = $"LOWER(Nombre) LIKE '%{filtroSeguro.ToLower()}%' AND Activo = 1";

                // Seleccionar las columnas que quieres mostrar
                string columnas = "Id, Nombre, Precio, Stock";

                return crud.ConsultarCondicion(columnas, "Productos", condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la búsqueda: {ex.Message}");
            }
        }
        public bool CrearProducto(Producto producto) //INSERTAR
        {
            try
            {
                string campos = "Nombre, Precio, Stock";
                string valores = $"'{producto.Nombre}', {producto.Precio}, {producto.Stock}";

                return crud.Insertar("Productos", campos, valores);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear producto: {ex.Message}");
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                string campos = $"Nombre='{producto.Nombre}', Precio={producto.Precio}, Stock={producto.Stock}";
                string condicion = $"Id = {producto.Id}";

                return crud.Actualizar("Productos", campos, condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar producto: {ex.Message}");
            }
        }

        public bool EliminarProducto(int id)
        {
            try
            {
                string condicion = $"Id = {id}";
                string campos = "Activo = 0"; // Eliminación lógica
                return crud.Actualizar("Productos", campos, condicion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar producto: {ex.Message}");
            }
        }
    }
}
