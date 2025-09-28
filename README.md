# 🖥️ CRUD con Patrón MVC en C# (WinForms .NET Framework 4.8)

Este proyecto implementa un sistema de gestión de productos utilizando **C#**, **Windows Forms** y el **patrón de diseño MVC**.  
Permite realizar operaciones **CRUD** (Crear, Leer, Actualizar y Eliminar) sobre una tabla de productos con validaciones y una interfaz amigable.

---

## 🚀 Características

- Arquitectura basada en el **Patrón MVC**.
- Interfaz de usuario desarrollada con **Windows Forms**.
- Operaciones CRUD completas:
  - Crear productos.
  - Listar productos.
  - Actualizar productos existentes.
  - Eliminar productos.
- Búsqueda de productos por nombre.
- Validaciones:
  - Campos obligatorios.
  - Precio mayor a 0.
  - Stock no negativo.
  - Nombres únicos de productos.
- Personalización de `DataGridView` con estilos visuales.

---

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Framework:** .NET Framework 4.8
- **IDE:** Visual Studio 2022
- **Patrón de diseño:** MVC (Modelo - Vista - Controlador)

---

## 📂 Estructura del proyecto

CRUD_PatronMVC/

│── Controllers/ # Controladores para manejar la lógica

│── Helpers/ # Métodos de validación y utilidades

│── Models/ # Clases que representan los datos (POCOs)

│── Views/ # Formularios de Windows Forms

│── Program.cs # Punto de entrada de la aplicación



---

## 📸 Capturas de pantalla


---

## ▶️ Ejecución

1. Clona el repositorio:
   ```bash
   git clone https://github.com/mariodiaz-sv/Patron-MVC-CRUD-manejo-de-roles-y-sesiones-en-C-VS-2022.git

   Abre el proyecto en Visual Studio 2022.

2. Restaura los paquetes NuGet si es necesario.

3. Cambia tu cadena de conexión dentrol del archivo ClaseCRUD

4. Compila y ejecuta el proyecto (Ctrl + F5).

5. Usuario Admin Contraseña Admin123

 
 ---


## 📖 Uso
**Agregar:** Completa los campos Nombre, Precio, Stock y presiona Agregar.
**Actualizar:** Selecciona un producto en el grid, modifica los datos y presiona Actualizar.
**Eliminar:** Selecciona un producto en el grid y presiona Eliminar.
**Buscar:** Ingresa el nombre de un producto y presiona Buscar.
**Ver todos:** Muestra nuevamente todos los productos registrados.
**Limpiar:** Restablece los campos del formulario.

---

## 📌 Autor
👤 Mario Díaz
🔗 GitHub: mariodiaz-sv

---

## 📝 Licencia
Este proyecto se distribuye bajo la licencia MIT.

---
