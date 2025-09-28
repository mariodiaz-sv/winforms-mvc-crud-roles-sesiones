using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CRUD_PatronMVC.Helpers
{
    public static class Validaciones
    {
        #region Validaciones Básicas de Campos
        /// <summary>
        /// Valida que un TextBox no esté vacío o con solo espacios
        /// </summary>
        public static bool ValidarNoVacio(TextBox textBox, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show($"{nombreCampo} es requerido.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que un ComboBox tenga una selección válida
        /// </summary>
        public static bool ValidarComboBox(ComboBox comboBox, string nombreCampo = "Este campo")
        {
            if (comboBox.SelectedIndex == -1 || comboBox.SelectedValue == null)
            {
                MessageBox.Show($"Debe seleccionar un valor para {nombreCampo}.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox.Focus();
                return false;
            }
            return true;
        }
        public static bool ValidarComboBox2(ComboBox comboBox, string nombreCampo = "Este campo")
        {
            // ✅ Validación MUY simple
            if (comboBox == null)
            {
                MessageBox.Show($"Error: El control {nombreCampo} no está inicializado.", "Error");
                return false;
            }

            // ✅ Solo verificar que haya algo seleccionado
            if (comboBox.SelectedItem == null && string.IsNullOrWhiteSpace(comboBox.Text))
            {
                MessageBox.Show($"Debe seleccionar un valor para {nombreCampo}.", "Validación");
                comboBox.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Validaciones de Tipos de Datos
        /// <summary>
        /// Valida que el texto contenga solo letras (incluye acentos y ñ)
        /// </summary>
        public static bool ValidarSoloLetras(string texto, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(texto)) return true; // Si está vacío, no validar aquí

            if (!Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras y espacios.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que el texto contenga solo números enteros
        /// </summary>
        public static bool ValidarSoloNumeros(string texto, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(texto)) return true;

            if (!Regex.IsMatch(texto, @"^\d+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener números enteros.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida números decimales con un punto decimal opcional
        /// </summary>
        public static bool ValidarNumeroDecimal(string texto, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(texto)) return true;

            // Permite: 123, 123.45, .45, 0.45
            if (!Regex.IsMatch(texto, @"^\d*\.?\d+$|^\d+\.?\d*$"))
            {
                MessageBox.Show($"{nombreCampo} debe ser un número válido (ej: 150.50).", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que no tenga más de 2 decimales
            if (texto.Contains("."))
            {
                string[] partes = texto.Split('.');
                if (partes.Length > 1 && partes[1].Length > 2)
                {
                    MessageBox.Show($"{nombreCampo} no puede tener más de 2 decimales.", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Valida y convierte a decimal, retorna el valor y un booleano de éxito
        /// </summary>
        public static (bool esValido, decimal valor) ValidarYConvertirDecimal(string texto, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(texto))
                return (false, 0);

            if (!ValidarNumeroDecimal(texto, nombreCampo))
                return (false, 0);

            if (decimal.TryParse(texto, out decimal resultado))
            {
                if (resultado >= 0)
                    return (true, resultado);
                else
                {
                    MessageBox.Show($"{nombreCampo} no puede ser negativo.", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return (false, 0);
                }
            }

            return (false, 0);
        }
        #endregion

        #region Validaciones Específicas para Entidades
        /// <summary>
        /// Valida formato de email
        /// </summary>
        public static bool ValidarEmail(string email, string nombreCampo = "Email")
        {
            if (string.IsNullOrWhiteSpace(email)) return true;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    // Validación adicional de formato básico
                    if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                        return true;
                }
            }
            catch { }

            MessageBox.Show($"El formato de {nombreCampo} no es válido.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Valida formato de teléfono
        /// </summary>
        public static bool ValidarTelefono(string telefono, string nombreCampo = "Teléfono")
        {
            if (string.IsNullOrWhiteSpace(telefono)) return true;

            // Permite: 1234567890, +52 123 456 7890, (123) 456-7890
            if (Regex.IsMatch(telefono, @"^[\+]?[0-9\s\-\(\)]{10,15}$"))
                return true;

            MessageBox.Show($"El formato de {nombreCampo} no es válido. Use solo números, espacios, guiones y paréntesis.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Valida fortaleza de contraseña
        /// </summary>
        public static bool ValidarFortalezaPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Mínimo 6 caracteres, al menos una letra y un número
            if (password.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra y un número.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region Validaciones de Longitud
        /// <summary>
        /// Valida longitud mínima y máxima
        /// </summary>
        public static bool ValidarLongitud(string texto, int min, int max, string nombreCampo = "Este campo")
        {
            if (string.IsNullOrWhiteSpace(texto)) return true;

            if (texto.Length < min)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos {min} caracteres.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (texto.Length > max)
            {
                MessageBox.Show($"{nombreCampo} no puede exceder {max} caracteres.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida rango numérico
        /// </summary>
        public static bool ValidarRango(decimal numero, decimal min, decimal max, string nombreCampo = "Este campo")
        {
            if (numero < min || numero > max)
            {
                MessageBox.Show($"{nombreCampo} debe estar entre {min} y {max}.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region Event Handlers para Validación en Tiempo Real
        /// <summary>
        /// Evento para validar solo letras mientras se escribe
        /// </summary>
        public static void TextBoxSoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento para validar solo números mientras se escribe
        /// </summary>
        public static void TextBoxSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento para validar números decimales mientras se escribe
        /// </summary>
        public static void TextBoxDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Permitir números, punto decimal, y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // No permitir punto decimal al inicio
            if (e.KeyChar == '.' && textBox.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            // Validar máximo 2 decimales
            if (textBox.Text.Contains("."))
            {
                int indexPunto = textBox.Text.IndexOf('.');
                if (textBox.SelectionStart > indexPunto &&
                    textBox.Text.Length - indexPunto > 2 &&
                    !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento para validar email mientras se escribe
        /// </summary>
        public static void TextBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir caracteres comunes en emails
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '@' && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '_')
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Métodos de Extensión para Controles
        /// <summary>
        /// Limpia todos los TextBox de un contenedor
        /// </summary>
        public static void LimpiarControles(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
                else if (control is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;
                else if (control is CheckBox checkBox)
                    checkBox.Checked = false;
                else if (control is DateTimePicker dateTimePicker)
                    dateTimePicker.Value = DateTime.Now;
                else if (control.HasChildren)
                    LimpiarControles(control);
            }
        }

        /// <summary>
        /// Habilita/Deshabilita todos los controles de un contenedor
        /// </summary>
        public static void SetEstadoControles(Control contenedor, bool habilitado)
        {
            foreach (Control control in contenedor.Controls)
            {
                control.Enabled = habilitado;
                if (control.HasChildren)
                    SetEstadoControles(control, habilitado);
            }
        }
        #endregion
    }
}