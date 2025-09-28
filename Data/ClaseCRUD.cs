using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PatronMVC.Data
{
    class ClaseCRUD
    {
        //Definir la cadena de conexióna
        private string Cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ingma\source\repos\CRUD_PatronMVC\Database\MiBase.mdf;Integrated Security=True";
        public SqlConnection Conexion; //conexión abierta con una base de datos de SQL Server.
        private SqlCommandBuilder cmb; //Compilar los cambios generados en una tabla
        public DataSet ds = new DataSet(); //Almacenar datos en memoria
        public SqlDataAdapter da; //Rellena un DataSet y actualizar una base de datos (Puente entre BD y DataSet).
        public SqlCommand Comando; //Instrucción de Transact-SQL que se ejecuta en una base de datos de SQL Server.

        //Método para conectar a la BD
        private void Conectar()
        {
            Conexion = new SqlConnection(Cadena);
        }

        //Constructor para conectar, debe tener el mismo nombre de la Clase
        public ClaseCRUD()
        {
            Conectar();
        }

        //Método para insertar nuevo registros a una tabla
        public bool Insertar(string Tabla, string Campos, string Valores)
        {
            Conexion.Open();
            string querySQL = "INSERT INTO " + Tabla + "(" + Campos + ") VALUES(" + Valores + ");"; ;
            Comando = new SqlCommand(querySQL, Conexion);
            int i = Comando.ExecuteNonQuery(); //Ejecuta sentencia dentro del servidor
            Conexion.Close();
            if (i > 0) //Evaluar si la variable i devuelve un valor mayor a cero
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Método para generar una consulta de una tabla en específica
        public DataTable Consultar(string Campos, string Tabla)
        {
            string querySQL = "SELECT " + Campos + " FROM " + Tabla;
            da = new SqlDataAdapter(querySQL, Conexion);
            DataSet dts = new DataSet();
            da.Fill(dts, Tabla); //Fill rellena con los datos del DataSet
            DataTable dt = new DataTable();
            dt = dts.Tables[Tabla]; //Corchetes porque las tablas son arreglos
            return dt;
        }

        //Método para realizar todo tipo de consulta
        public void ConsultarGral(string querySQL, string Tabla)
        {
            ds.Tables.Clear();  //Limpiar contenido del DataSet de cualquier información
            da = new SqlDataAdapter(querySQL, Conexion);
            cmb = new SqlCommandBuilder(da);
            da.Fill(ds, Tabla);
        }

        //Método para eliminar registro de una tabla
        public bool Eliminar(string Tabla, string Condicion)
        {
            Conexion.Open();
            string querySQL = "DELETE FROM " + Tabla + " WHERE " + Condicion;
            Comando = new SqlCommand(querySQL, Conexion);
            int i = Comando.ExecuteNonQuery(); //Ejecuta sentencia dentro del servidor
            Conexion.Close();
            if (i > 0) //Evaluar si la variable i devuelve un valor mayor a cero
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Método para generar una consulta definiendo una condición
        public DataTable ConsultarCondicion(string Campos, string Tabla, string Condicion)
        {
            string querySQL = "SELECT " + Campos + " FROM " + Tabla + " WHERE " + Condicion;
            da = new SqlDataAdapter(querySQL, Conexion);
            DataSet dts = new DataSet();
            da.Fill(dts, Tabla);
            DataTable dt = new DataTable();
            dt = dts.Tables[Tabla]; //Corchetes porque las tablas son arreglos
            return dt;
        }

        //Método para actualizar registro en una tabla
        public bool Actualizar(string Tabla, string Campos, string Condicion)
        {
            Conexion.Open();
            string querySQL = "UPDATE " + Tabla + " SET " + Campos + " WHERE " + Condicion;
            Comando = new SqlCommand(querySQL, Conexion);
            int i = Comando.ExecuteNonQuery(); //Ejecuta sentencia dentro del servidor
            Conexion.Close();
            if (i > 0) //Evaluar si la variable i devuelve un valor mayor a cero
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
