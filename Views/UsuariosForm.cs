using CRUD_PatronMVC.Controllers;
using CRUD_PatronMVC.Helpers;
using CRUD_PatronMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CRUD_PatronMVC.Views
{
    public partial class UsuariosForm : Form
    {
        private int usuarioIdSeleccionado = 0; // ← Variable para el ID del usuario seleccionado
        private UsuarioController usuarioController;
        private DataTable dtUsuarios;
        private DataTable dtRoles;
        public UsuariosForm()
        {
            InitializeComponent();
            usuarioController = new UsuarioController();
            CargarRoles();
            PersonalizarFormulario();
            CargarUsuarios();
            CargarSexo();
            ConfigurarModoInicial(); // ← Nuevo método
        }
        private void ConfigurarModoInicial()
        {
            // Configurar estado inicial de botones
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false; // Si tienes botón eliminar
        }
        //metodo para cambiar el color de los textbox
        private void PersonalizarFormulario()
        {
            txtUsuario.Enter += (s, e) => { txtUsuario.BackColor = Color.LightCyan; };
            txtUsuario.Leave += (s, e) => { txtUsuario.BackColor = Color.White; };
            txtPassword.Enter += (s, e) => { txtPassword.BackColor = Color.LightCyan; };
            txtPassword.Leave += (s, e) => { txtPassword.BackColor = Color.White; };
            txtNombre.Enter += (s, e) => { txtNombre.BackColor = Color.LightCyan; };
            txtNombre.Leave += (s, e) => { txtNombre.BackColor = Color.White; };
            txtApellido.Enter += (s, e) => { txtApellido.BackColor = Color.LightCyan; };
            txtApellido.Leave += (s, e) => { txtApellido.BackColor = Color.White; };
            txtTelefono.Enter += (s, e) => { txtTelefono.BackColor = Color.LightCyan; };
            txtTelefono.Leave += (s, e) => { txtTelefono.BackColor = Color.White; };
            txtCorreo.Enter += (s, e) => { txtCorreo.BackColor = Color.LightCyan; };
            txtCorreo.Leave += (s, e) => { txtCorreo.BackColor = Color.White; };

        }

        private void CargarSexo() //metodo para agregar H hombre, M mujer en el combobox sexo
        {
            cmbSexo.Items.Clear();

            // ✅ Usar exactamente lo que espera la base de datos
            cmbSexo.Items.Add("Masculino");
            cmbSexo.Items.Add("Femenino");

            cmbSexo.SelectedIndex = 0; // "Masculino" por defecto

            // ✅ Opcional: Configurar como DropDownList para evitar texto manual
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        //cargar roles desde la BD
        private void CargarRoles()
        {
            try
            {
                // ✅ NUEVA FORMA (Con RolController especializado):
                RolController rolController = new RolController();
                dtRoles = rolController.ObtenerRolesParaComboBox();

                cmbRol.DataSource = dtRoles;
                cmbRol.DisplayMember = "Nombre";
                cmbRol.ValueMember = "Id";

                // Agregar opción por defecto
                if (cmbRol.Items.Count > 0)
                {
                    cmbRol.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                dtUsuarios = usuarioController.ObtenerTodosUsuarios();

                // ✅ VERIFICAR que hay datos antes de asignar
                if (dtUsuarios != null && dtUsuarios.Rows.Count > 0)
                {
                    dgvUsuarios.DataSource = dtUsuarios;
                    ConfigurarDataGridView();
                }
                else
                {
                    dgvUsuarios.DataSource = null;
                    MessageBox.Show("No se encontraron usuarios registrados.", "Información",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigurarDataGridView()
        {
            
            try
            {
                dgvUsuarios.SuspendLayout();

                dgvUsuarios.AutoGenerateColumns = false; // <- control total sobre columnas
                dgvUsuarios.Columns.Clear();

                // Definir columnas en el orden deseado
                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Usuario",
                    HeaderText = "Usuario",
                    DataPropertyName = "Usuario",
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Rol",
                    HeaderText = "Rol",
                    // la consulta debe traer "Rol" o lo agregamos manualmente
                    // antes de asignar DataSource
                    DataPropertyName = "Rol", 
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    DataPropertyName = "Nombre",
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    DataPropertyName = "Apellido",
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Sexo",
                    HeaderText = "Sexo",
                    DataPropertyName = "Sexo",
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Telefono",
                    HeaderText = "Teléfono",
                    DataPropertyName = "Telefono",
                    ReadOnly = true
                });

                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Correo",
                    HeaderText = "Correo Electrónico",
                    DataPropertyName = "Correo",
                    ReadOnly = true
                });

                // Columna Id (oculta) — útil para operaciones CRUD
                dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    HeaderText = "Id",
                    DataPropertyName = "Id",
                    ReadOnly = true,
                    Visible = false
                });

                // Propiedades visuales generales
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;
                dgvUsuarios.AllowUserToDeleteRows = false;
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuarios.BackgroundColor = Color.White;
                dgvUsuarios.BorderStyle = BorderStyle.None;
                dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dgvUsuarios.RowHeadersVisible = false;

                dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                dgvUsuarios.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvUsuarios.ResumeLayout();
            }
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                // ✅ Guardar el ID del usuario seleccionado
                if (fila.Cells["Id"].Value != null)
                {
                    usuarioIdSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
                }

                // ✅ Llenar los campos
                txtUsuario.Text = fila.Cells["Usuario"].Value?.ToString() ?? "";
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Text = fila.Cells["Apellido"].Value?.ToString() ?? "";
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString() ?? "";
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";

                // ✅ ComboBox Sexo
                string sexo = fila.Cells["Sexo"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(sexo))
                {
                    cmbSexo.Text = sexo;
                }

                // ✅ ComboBox Rol
                string nombreRol = fila.Cells["Rol"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(nombreRol))
                {
                    foreach (DataRowView item in cmbRol.Items)
                    {
                        if (item["Nombre"].ToString() == nombreRol)
                        {
                            cmbRol.SelectedValue = item["Id"];
                            break;
                        }
                    }
                }

                // ✅ Password placeholder
                txtPassword.Text = "********";
                txtPassword.Tag = "edicion";

                // ✅ Cambiar a modo edición
                CambiarColorFormularioEdicion();
                btnActualizar.Enabled = true; // Habilitar botón actualizar
                btnEliminar.Enabled = true; // Si tienes botón eliminar
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del usuario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CambiarColorFormularioEdicion()
        {
            Color colorEdicion = Color.LightYellow;
            txtUsuario.BackColor = colorEdicion;
            txtNombre.BackColor = colorEdicion;
            txtApellido.BackColor = colorEdicion;
        }

        private void RestaurarColorFormulario()
        {
            Color colorNormal = Color.White;
            txtUsuario.BackColor = colorNormal;
            txtNombre.BackColor = colorNormal;
            txtApellido.BackColor = colorNormal;
        }
       

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
               // CargarDatosUsuario(Convert.ToInt32(row.Cells["Id"].Value));
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ VERIFICAR que no esté en modo edición
                if (usuarioIdSeleccionado > 0)
                {
                    MessageBox.Show("Está en modo edición. Para crear un nuevo usuario, " +
                        "primero llene el formulario con los datos correctos.","Modo Edición",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    return;
                }
                // Validaciones usando Helpers
                // ✅ VALIDACIONES BÁSICAS DE TEXTBOX
                if (!Validaciones.ValidarNoVacio(txtNombre, "Nombre")) return;
                if (!Validaciones.ValidarSoloLetras(txtNombre.Text, "Nombre")) return;
                if (!Validaciones.ValidarNoVacio(txtApellido, "Apellido")) return;
                if (!Validaciones.ValidarSoloLetras(txtApellido.Text, "Apellido")) return;
                if (!Validaciones.ValidarNoVacio(txtTelefono, "Telefono")) return;
                if (!Validaciones.ValidarSoloNumeros(txtTelefono.Text, "Teléfono")) return;
                if (!Validaciones.ValidarNoVacio(txtCorreo, "Correo electronico")) return;
                if (!Validaciones.ValidarEmail(txtCorreo.Text, "Correo electrónico")) return;

                // ✅ VALIDACIÓN DE COMBOBOX - TU CÓDIGO ESTÁ BIEN
                if (!Validaciones.ValidarComboBox2(cmbSexo, "Sexo")) return;


                // ✅ VALIDACIONES ESPECÍFICAS
                if (!Validaciones.ValidarNoVacio(txtUsuario, "Usuario")) return;
                // ✅ VALIDACIÓN DE PASSWORD (solo para nuevos usuarios)

                if (!Validaciones.ValidarNoVacio(txtPassword, "Contraseña")) return;
                if (!Validaciones.ValidarFortalezaPassword(txtPassword.Text)) return;

                if (!Validaciones.ValidarComboBox(cmbRol, "Rol")) return;
                // ✅ SI PASA TODAS LAS VALIDACIONES, PROCEDER CON GUARDAR


                Usuario usuario = new Usuario
                {
                    Username = txtUsuario.Text,
                    PasswordHash = AuthController.GenerarHashSHA256(txtPassword.Text),
                    IdRol = Convert.ToInt32(cmbRol.SelectedValue),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Sexo = cmbSexo.SelectedItem.ToString(),
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text
                };

                bool resultado = usuarioController.CrearUsuario(usuario);

                if (resultado)
                {
                    MessageBox.Show("Usuario creado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarUsuarioUnico(string usuario, int idExcluir = 0)
        {
            try
            {
                // Verificar si el usuario ya existe (excluyendo el idExcluir para actualizaciones)
                string condicion = $"Usuario = '{usuario.Replace("'", "''")}' AND Activo = 1";

                if (idExcluir > 0)
                {
                    condicion += $" AND Id != {idExcluir}";
                }

                DataTable usuariosExistentes = usuarioController.ObtenerUsuariosPorCondicion(condicion);

                if (usuariosExistentes.Rows.Count > 0)
                {
                    MessageBox.Show("Ya existe un usuario con ese nombre de usuario. Por favor elija otro.",
                                  "Usuario Duplicado",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar usuario único: {ex.Message}", "Error");
                return false;
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ VERIFICAR que haya un usuario seleccionado
                if (usuarioIdSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario de la lista para actualizar.",
                                  "Selección Requerida",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ VALIDACIONES BÁSICAS DE TEXTBOX
                if (!Validaciones.ValidarNoVacio(txtNombre, "Nombre")) return;
                if (!Validaciones.ValidarSoloLetras(txtNombre.Text, "Nombre")) return;
                if (!Validaciones.ValidarNoVacio(txtApellido, "Apellido")) return;
                if (!Validaciones.ValidarSoloLetras(txtApellido.Text, "Apellido")) return;
                if (!Validaciones.ValidarNoVacio(txtTelefono, "Telefono")) return;
                if (!Validaciones.ValidarSoloNumeros(txtTelefono.Text, "Teléfono")) return;
                if (!Validaciones.ValidarNoVacio(txtCorreo, "Correo electronico")) return;
                if (!Validaciones.ValidarEmail(txtCorreo.Text, "Correo electrónico")) return;

                // ✅ VALIDACIÓN DE COMBOBOX
                if (!Validaciones.ValidarComboBox2(cmbSexo, "Sexo")) return;
                if (!Validaciones.ValidarComboBox(cmbRol, "Rol")) return;

                // ✅ VALIDACIONES ESPECÍFICAS
                if (!Validaciones.ValidarNoVacio(txtUsuario, "Usuario")) return;

                // ✅ VALIDACIÓN DE USUARIO ÚNICO (excluyendo el usuario actual)
                if (!ValidarUsuarioUnico(txtUsuario.Text, usuarioIdSeleccionado)) return;

                // ✅ VALIDACIÓN OPCIONAL DE CONTRASEÑA (solo si se cambió)
                string nuevoPasswordHash = null;
                if (!string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text != "********")
                {
                    if (!Validaciones.ValidarFortalezaPassword(txtPassword.Text)) return;
                    nuevoPasswordHash = AuthController.GenerarHashSHA256(txtPassword.Text);
                }

                // ✅ CONFIRMACIÓN
                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de actualizar los datos del usuario: {txtNombre.Text} {txtApellido.Text}?",
                    "Confirmar Actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes) return;

                // ✅ ACTUALIZAR USUARIO
                Usuario usuario = new Usuario
                {
                    Id = usuarioIdSeleccionado, // ← ID del usuario seleccionado
                    Username = txtUsuario.Text,
                    PasswordHash = nuevoPasswordHash, // ← Si es null, no se actualiza la contraseña
                    IdRol = Convert.ToInt32(cmbRol.SelectedValue),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Sexo = cmbSexo.SelectedItem.ToString(),
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text
                };

                bool resultado = usuarioController.ActualizarUsuario(usuario);

                if (resultado)
                {
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario. Verifique los datos.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///
        private void LimpiarFormulario()
        {
            // ✅ Limpiar variable de ID
            usuarioIdSeleccionado = 0;

            // ✅ Limpiar campos
            txtUsuario.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtPassword.Clear();
            txtPassword.Tag = null;
            cmbSexo.SelectedIndex = 0;
            cmbRol.SelectedIndex = 0;

            // ✅ Restaurar color original
            RestaurarColorFormulario();

            // ✅ Volver a modo agregar
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false; // ← Deshabilitar eliminar también

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Validación básica
                if (usuarioIdSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un usuario para eliminar.");
                    return;
                }

                // ✅ Confirmación simple
                var confirmacion = MessageBox.Show(
                    $"¿Eliminar al usuario {txtNombre.Text} {txtApellido.Text}?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes) return;

                // ✅ Ejecutar eliminación
                bool resultado = usuarioController.EliminarUsuario(usuarioIdSeleccionado);

                if (resultado)
                {
                    MessageBox.Show("Usuario eliminado exitosamente.");
                    LimpiarFormulario();
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtUsuario.Text.Trim();

                if (!Validaciones.ValidarNoVacio(txtUsuario, "Usuario a buscar"))
                    return;

                DataTable dt = usuarioController.BuscarUsuariosPorNombreUsuario(filtro);
                LimpiarFormulario();
                // Validar resultados
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontró el usuario '{filtro}'.",
                                    "Sin resultados",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    dgvUsuarios.DataSource = null;
                    return;
                }

                // Si la consulta devuelve IdRol en lugar de Rol, creamos la columna "Rol" y la llenamos
                if (!dt.Columns.Contains("Rol") && dt.Columns.Contains("IdRol"))
                {
                    dt.Columns.Add("Rol", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["IdRol"] == DBNull.Value)
                        {
                            row["Rol"] = string.Empty;
                            continue;
                        }

                        int idRol;
                        if (int.TryParse(row["IdRol"].ToString(), out idRol))
                        {
                            // Intentamos obtener el nombre del rol desde cmbRol (si está cargado)
                            string nombreRol = ObtenerNombreRolDesdeCombo(idRol);
                            if (!string.IsNullOrEmpty(nombreRol))
                            {
                                row["Rol"] = nombreRol;
                            }
                            else
                            {
                                // Fallback: mostrar el IdRol si no hay nombre disponible
                                row["Rol"] = idRol.ToString();
                            }
                        }
                    }
                }

                // Si la consulta devuelve otras columnas con nombres diferentes, podrías mapearlas aquí
                // Por ejemplo: si la BD devuelve "CorreoElectronico" -> dt.Columns["CorreoElectronico"] -> copiar a "Correo".
                if (!dt.Columns.Contains("Correo") && dt.Columns.Contains("CorreoElectronico"))
                {
                    dt.Columns.Add("Correo", typeof(string));
                    foreach (DataRow row in dt.Rows)
                        row["Correo"] = row["CorreoElectronico"];
                }

                // Asignamos la fuente de datos (las columnas que definimos en ConfigurarDataGridView se enlazan por DataPropertyName)
                dgvUsuarios.DataSource = dt;

                MessageBox.Show($"Se encontraron {dt.Rows.Count} usuario(s).",
                                "Búsqueda exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca el nombre del rol en el ComboBox cmbRol (suponiendo que cmbRol.ValueMember = "Id" y DisplayMember = "Nombre").
        /// Devuelve null si no lo encuentra.
        /// </summary>
        private string ObtenerNombreRolDesdeCombo(int idRol)
        {
            try
            {
                if (cmbRol.DataSource == null) return null;

                // Si DataSource es DataTable
                if (cmbRol.DataSource is DataTable dtRoles)
                {
                    DataRow[] rows = dtRoles.Select($"Id = {idRol}");
                    if (rows.Length > 0) return rows[0]["Nombre"].ToString();
                }

                // Si DataSource es BindingSource -> DataSource interno puede ser DataTable o List<T>
                if (cmbRol.DataSource is BindingSource bs)
                {
                    if (bs.DataSource is DataTable dt2)
                    {
                        DataRow[] rows = dt2.Select($"Id = {idRol}");
                        if (rows.Length > 0) return rows[0]["Nombre"].ToString();
                    }
                    else if (bs.DataSource is IEnumerable<object> listObj)
                    {
                        foreach (var item in listObj)
                        {
                            var propId = item.GetType().GetProperty("Id");
                            var propNombre = item.GetType().GetProperty("Nombre");
                            if (propId != null && propNombre != null)
                            {
                                var val = propId.GetValue(item);
                                if (val != null && Convert.ToInt32(val) == idRol)
                                    return propNombre.GetValue(item)?.ToString();
                            }
                        }
                    }
                }

                // Si DataSource es lista genérica
                if (cmbRol.Items.Count > 0)
                {
                    foreach (var it in cmbRol.Items)
                    {
                        var propId = it.GetType().GetProperty("Id");
                        var propNombre = it.GetType().GetProperty("Nombre");
                        if (propId != null && propNombre != null)
                        {
                            var val = propId.GetValue(it);
                            if (val != null && Convert.ToInt32(val) == idRol)
                                return propNombre.GetValue(it)?.ToString();
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
            

        }
       

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar el texto de búsqueda
                txtUsuario.Clear();

                // Recargar todos los usuarios
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}
