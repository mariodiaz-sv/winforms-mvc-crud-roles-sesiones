using CRUD_PatronMVC.Controllers;
using CRUD_PatronMVC.Models;
using CRUD_PatronMVC.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_PatronMVC
{
    public partial class LoginForm : Form
    {
        private AuthController authController;

        public LoginForm()
        {
            InitializeComponent();
            // ✅ CORRECCIÓN: Inicializar authController en el constructor
            authController = new AuthController();
            PersonalizarFormulario();
        }
        private void PersonalizarFormulario()
        {
            //cambiar el color de los textbox
            txtUsuario.Enter += (s, e) => { txtUsuario.BackColor = Color.LightCyan; };
            txtUsuario.Leave += (s, e) => { txtUsuario.BackColor = Color.White; };
            txtPassword.Enter += (s, e) => { txtPassword.BackColor = Color.LightCyan; };
            txtPassword.Leave += (s, e) => { txtPassword.BackColor = Color.White; };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ VERIFICACIÓN ADICIONAL: Confirmar que authController no es null
                if (authController == null)
                {
                    MessageBox.Show("Error: Controlador de autenticación no inicializado.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (authController.Autenticar(txtUsuario.Text, txtPassword.Text))
                {
                    MessageBox.Show($"Bienvenido {Sesion.NombreCompleto} ({Sesion.RolNombre})",
                                  "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DashboardForm dashboard = new DashboardForm();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Login",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // ✅ MEJOR MANEJO DE ERRORES
                MessageBox.Show($"Error durante el login: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
