using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUD_PatronMVC.Controllers;
using CRUD_PatronMVC.Helpers;
using CRUD_PatronMVC.Models;

namespace CRUD_PatronMVC.Views
{
    public partial class ProductosForm : Form
    {
        private int productoIdSeleccionado = 0;
        private ProductoController productoController;
        private DataTable dtProductos;
        public ProductosForm()
        {
            InitializeComponent();
            productoController = new ProductoController();
            PersonalizarFormulario();
            ConfigurarModoInicial();
            CargarProductos();
        }
        private void ConfigurarModoInicial()
        {
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void PersonalizarFormulario()
        {
            // Personalizar colores al entrar/salir de los TextBox
            txtNombre.Enter += (s, e) => { txtNombre.BackColor = Color.LightCyan; };
            txtNombre.Leave += (s, e) => { txtNombre.BackColor = Color.White; };

            txtPrecio.Enter += (s, e) => { txtPrecio.BackColor = Color.LightCyan; };
            txtPrecio.Leave += (s, e) => { txtPrecio.BackColor = Color.White; };

            txtStock.Enter += (s, e) => { txtStock.BackColor = Color.LightCyan; };
            txtStock.Leave += (s, e) => { txtStock.BackColor = Color.White; };
        }

        private void CargarProductos()
        {
            try
            {
                dtProductos = productoController.ObtenerTodosProductos();

                if (dtProductos != null && dtProductos.Rows.Count > 0)
                {
                    // ✅ Asegurar que la columna Precio sea de tipo decimal
                    if (dtProductos.Columns.Contains("Precio"))
                    {
                        dtProductos.Columns["Precio"].DataType = typeof(decimal);
                    }

                    dgvProductos.DataSource = dtProductos;
                    ConfigurarDataGridView();
                }
                else
                {
                    dgvProductos.DataSource = null;
                    MessageBox.Show("No se encontraron productos registrados.", "Información",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                dgvProductos.SuspendLayout();

                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.Columns.Clear();
               
                // Columna Nombre
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre del Producto",
                    DataPropertyName = "Nombre",
                    ReadOnly = true
                });

                // Columna Precio con formato de moneda
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Precio",
                    HeaderText = "Precio",
                    DataPropertyName = "Precio",
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2", // Formato de moneda con 2 decimales
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                // Columna Stock
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Stock",
                    HeaderText = "Stock",
                    DataPropertyName = "Stock",
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                // Columna Id (oculta)
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    HeaderText = "Id",
                    DataPropertyName = "Id",
                    ReadOnly = true,
                    Visible = false
                });

                // Propiedades visuales generales
                dgvProductos.ReadOnly = true;
                dgvProductos.AllowUserToAddRows = false;
                dgvProductos.AllowUserToDeleteRows = false;
                dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductos.MultiSelect = false;

                // ✅ CONFIGURACIÓN DE AJUSTE DE COLUMNAS
                // Configuración mínima para autoajuste con Fill
                dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Opcional: Ajustar manualmente alguna columna específica
                dgvProductos.Columns["Id"].FillWeight = 10; // 15% del espacio disponible
                dgvProductos.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvProductos.Columns["Precio"].FillWeight = 15; // 15% del espacio disponible
                dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvProductos.Columns["Stock"].FillWeight = 15; // 15% del espacio disponible
                dgvProductos.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgvProductos.BackgroundColor = Color.White;
                dgvProductos.BorderStyle = BorderStyle.None;
                dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dgvProductos.RowHeadersVisible = false;


                dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
                dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                dgvProductos.EnableHeadersVisualStyles = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvProductos.ResumeLayout();
            }

        }
        private void ProductosForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // Guardar el ID del producto seleccionado
                if (fila.Cells["Id"].Value != null)
                {
                    productoIdSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
                }

                // Llenar los campos
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtPrecio.Text = fila.Cells["Precio"].Value?.ToString() ?? "";
                txtStock.Text = fila.Cells["Stock"].Value?.ToString() ?? "";

                // Cambiar a modo edición
                CambiarColorFormularioEdicion();
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CambiarColorFormularioEdicion()
        {
            Color colorEdicion = Color.LightYellow;
            txtNombre.BackColor = colorEdicion;
            txtPrecio.BackColor = colorEdicion;
            txtStock.BackColor = colorEdicion;
        }

        private void RestaurarColorFormulario()
        {
            Color colorNormal = Color.White;
            txtNombre.BackColor = colorNormal;
            txtPrecio.BackColor = colorNormal;
            txtStock.BackColor = colorNormal;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que no esté en modo edición
                if (productoIdSeleccionado > 0)
                {
                    MessageBox.Show("Está en modo edición. Para crear un nuevo producto, primero limpie el formulario.",
                                  "Modo Edición",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Validaciones
                if (!Validaciones.ValidarNoVacio(txtNombre, "Nombre del producto")) return;
                if (!Validaciones.ValidarNoVacio(txtPrecio, "Precio")) return;
                if (!Validaciones.ValidarNumeroDecimal(txtPrecio.Text, "Precio")) return;
                if (!Validaciones.ValidarNoVacio(txtStock, "Stock")) return;
                if (!Validaciones.ValidarSoloNumeros(txtStock.Text, "Stock")) return;

                // Validar que el precio sea mayor a 0
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a 0.", "Error de validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                // Validar que el stock sea mayor o igual a 0
                int stock = Convert.ToInt32(txtStock.Text);
                if (stock < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.", "Error de validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                // Validar nombre único
                if (!ValidarProductoUnico(txtNombre.Text)) return;

                // Crear producto
                Producto producto = new Producto
                {
                    Nombre = txtNombre.Text,
                    Precio = precio,
                    Stock = stock
                };

                bool resultado = productoController.CrearProducto(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto creado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarProductoUnico(string nombre, int idExcluir = 0)
        {
            try
            {
                string condicion = $"Nombre = '{nombre.Replace("'", "''")}' AND Activo = 1";

                if (idExcluir > 0)
                {
                    condicion += $" AND Id != {idExcluir}";
                }

                DataTable productosExistentes = productoController.ObtenerProductosPorCondicion(condicion);

                if (productosExistentes.Rows.Count > 0)
                {
                    MessageBox.Show("Ya existe un producto con ese nombre. Por favor elija otro.",
                                  "Producto Duplicado",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    txtNombre.SelectAll();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar producto único: {ex.Message}", "Error");
                return false;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya un producto seleccionado
                if (productoIdSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar un producto de la lista para actualizar.",
                                  "Selección Requerida",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validaciones
                if (!Validaciones.ValidarNoVacio(txtNombre, "Nombre del producto")) return;
                if (!Validaciones.ValidarNoVacio(txtPrecio, "Precio")) return;
                if (!Validaciones.ValidarNumeroDecimal(txtPrecio.Text, "Precio")) return;
                if (!Validaciones.ValidarNoVacio(txtStock, "Stock")) return;
                if (!Validaciones.ValidarSoloNumeros(txtStock.Text, "Stock")) return;

                // Validaciones de negocio
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a 0.", "Error de validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                int stock = Convert.ToInt32(txtStock.Text);
                if (stock < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.", "Error de validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                // Validar nombre único (excluyendo el producto actual)
                if (!ValidarProductoUnico(txtNombre.Text, productoIdSeleccionado)) return;

                // Confirmación
                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de actualizar los datos del producto: {txtNombre.Text}?",
                    "Confirmar Actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes) return;

                // Actualizar producto
                Producto producto = new Producto
                {
                    Id = productoIdSeleccionado,
                    Nombre = txtNombre.Text,
                    Precio = precio,
                    Stock = stock
                };

                bool resultado = productoController.ActualizarProducto(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProductos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el producto. Verifique los datos.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (productoIdSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de eliminar el producto: {txtNombre.Text}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes) return;

                bool resultado = productoController.EliminarProducto(productoIdSeleccionado);

                if (resultado)
                {
                    MessageBox.Show("Producto eliminado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtNombre.Text.Trim();

                if (!Validaciones.ValidarNoVacio(txtNombre, "Nombre del producto a buscar"))
                    return;

                DataTable dt = productoController.BuscarProductosPorNombre(filtro);

                
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontró el producto '{filtro}'.",
                                    "Sin resultados",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    dgvProductos.DataSource = null;
                    LimpiarFormulario(); // limpiar formulario
                    return;
                }

                dgvProductos.DataSource = dt;
                LimpiarFormulario(); // limpiar formulario

                MessageBox.Show($"Se encontraron {dt.Rows.Count} producto(s).",
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

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            try
            {
                txtNombre.Clear();
                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
        private void LimpiarFormulario()
        {
            productoIdSeleccionado = 0;
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            RestaurarColorFormulario();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProductos.SelectedRows[0];
                // Puedes agregar lógica adicional si es necesario
            }
        }

      
    }
}
