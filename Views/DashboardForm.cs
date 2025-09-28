using CRUD_PatronMVC.Data;
using CRUD_PatronMVC.Models;//<--agregar
using System;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;


namespace CRUD_PatronMVC.Views
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            CargarInformacionUsuario();//add
            ConfigurarBotonesSegunRol();//add
        }
        //metodo para mostrar los datos de inicio de sesion
        private void CargarInformacionUsuario()
        {
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                string nombreCompleto = $"{Sesion.NombreCompleto} {Sesion.Username}";
                // Mostrar en StatusLabel
                toolStripStatusLabel1.Text = $"Bienvenido: {Sesion.NombreCompleto} ({Sesion.RolNombre})";
                // Opcional: mostrar en la barra de título
              //  this.Text = $"Sistema - {nombreCompleto} ({Sesion.RolNombre})";
            }
        }
        private void ConfigurarBotonesSegunRol()
        {
            // Solo administradores pueden gestionar usuarios
            // Admin = 1
            if (Sesion.RolId != '1') // Admin
            {
                usuarioToolStripMenuItem.Enabled = true;
                productoToolStripMenuItem.Enabled = true;
                
            }
            else // Vendedor
            {
                usuarioToolStripMenuItem.Visible = false;
                productoToolStripMenuItem.Enabled = true;
            }
        }
        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //abrir el formualrio de Gestion de Usuarios
            UsuariosForm uf = new UsuariosForm();
            uf.ShowDialog();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //abrir el formualrio de Gestion de Productos
            ProductosForm pf = new ProductosForm();
            pf.ShowDialog();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                                           "Cerrar sesión",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Limpiar sesión
                Sesion.Limpiar();

                // Cerrar formulario actual
                this.Hide();

                // Abrir formulario de login
                LoginForm login = new LoginForm();
                login.Show();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                                            "Confirmar salida",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
                // Limpiar sesión
                Sesion.Limpiar();
            }
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
                Sesion.Limpiar();
            }
        }
    }
}
