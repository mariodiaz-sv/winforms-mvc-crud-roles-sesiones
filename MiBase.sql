-- Tabla Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);
GO

-- Tabla Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Usuario NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(64) NOT NULL, -- SHA256 produce 64 caracteres
    IdRol INT NOT NULL FOREIGN KEY REFERENCES Roles(Id),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Sexo NVARCHAR(10) NOT NULL CHECK (Sexo IN ('Masculino', 'Femenino')),
    Telefono NVARCHAR(15),
    Correo NVARCHAR(100),
    Activo BIT DEFAULT 1
);
GO

-- Tabla Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    Activo BIT DEFAULT 1
);
GO

-- Insertar roles
INSERT INTO Roles (Nombre) VALUES 
('Administrador'),('Vendedor');
GO

-- Insertar usuario admin (contraseña: admin123)
INSERT INTO Usuarios (Usuario, PasswordHash, IdRol, Nombre, Apellido, Sexo, Telefono, Correo) 
VALUES ('admin', '240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1, 'Admin', 'Sistema', 'Masculino', '123456789', 'admin@sistema.com');
GO

-- Insertar usuario vendedor (contraseña: vendedor123)
INSERT INTO Usuarios (Usuario, PasswordHash, IdRol, Nombre, Apellido, Sexo, Telefono, Correo) 
VALUES ('vendedor', '1C4B2B6D8F0A3E5C7D9E1B3F5D7E9A1C3B5D7F9E1B3D5F7A9C1E3B5D7F9A1C3', 2, 'Juan', 'Vendedor', 'Masculino', '987654321', 'vendedor@sistema.com');
GO
