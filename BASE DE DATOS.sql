-- RETO 01: Modelo de datos
CREATE DATABASE ComercioCoreDB;
GO

USE ComercioCoreDB;
GO

--Usuario para conexion con backend
CREATE LOGIN ComercioCoreUserDB 
    WITH PASSWORD = 'Password@12345#';
GO

CREATE USER ComercioCoreUserDB FOR LOGIN ComercioCoreUserDB;
GO

ALTER ROLE db_owner ADD MEMBER ComercioCoreUserDB;
GO

--Tabla Municipio
CREATE TABLE Municipio (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

-- Tabla Usuario
CREATE TABLE Usuario (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador', 'Auxiliar de Registro'))
);
GO

-- Tabla Comerciante
CREATE TABLE Comerciante (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial VARCHAR(100) NOT NULL,
    MunicipioId INT NOT NULL FOREIGN KEY REFERENCES Municipio(ID),
    Telefono VARCHAR(20) NULL,
    CorreoElectronico VARCHAR(100) NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(10) NOT NULL CHECK (Estado IN ('Activo', 'Inactivo')),
	--Campos auditoria
    FechaActualizacion DATETIME NULL,
    UsuarioActualizacion VARCHAR(100) NULL
);
GO

-- Tabla Establecimiento
CREATE TABLE Establecimiento (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NombreEstablecimiento VARCHAR(100) NOT NULL,
    Ingresos DECIMAL(18,2) NOT NULL,
    NumeroEmpleados INT NOT NULL,
    ComercianteID INT NOT NULL FOREIGN KEY REFERENCES Comerciante(ID),
	--Campos auditoria
    FechaActualizacion DATETIME NULL,
    UsuarioActualizacion VARCHAR(100) NULL
);
GO

-- RETO 02: Identificadores y Auditoría

-- Trigger para actualización de Comerciante
CREATE TRIGGER TRG_Comerciante_Auditoria
ON Comerciante
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE c
    SET FechaActualizacion = GETDATE(),
        UsuarioActualizacion = SUSER_NAME()
    FROM Comerciante c
    INNER JOIN inserted i ON c.ID = i.ID;
END;
GO

-- Trigger para actualización de Establecimiento
CREATE TRIGGER TRG_Establecimiento_Auditoria
ON Establecimiento
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE e
    SET FechaActualizacion = GETDATE(),
        UsuarioActualizacion = SUSER_NAME()
    FROM Establecimiento e
    INNER JOIN inserted i ON e.ID = i.ID;
END;
GO

-- RETO 03: Datos semilla

INSERT INTO Municipio (Nombre) VALUES
('Tuluá'),
('San Pedro'),
('Andalucia'),
('Buga'),
('Cartagena'),
('Bucaramanga'),
('Pereira'),
('Manizales'),
('Armenia'),
('Santa Marta');

INSERT INTO Usuario (Nombre, CorreoElectronico, Contrasena, Rol)
VALUES
('Admin Principal', 'admin@comercio.com', 'Admin123', 'Administrador'),
('Admin 2', 'admin2@comercio.com', 'Admin123', 'Administrador'),
('Auxiliar Registro 1', 'auxiliar1@comercio.com', 'Auxiliar123', 'Auxiliar de Registro'),
('Auxiliar Registro 2', 'auxiliar2@comercio.com', 'Auxiliar123', 'Auxiliar de Registro');
GO

INSERT INTO Comerciante (RazonSocial, MunicipioId, Telefono, CorreoElectronico, FechaRegistro, Estado)
VALUES
('Exito wow', 1, '3111111111', 'exitowow@yopmail.com', '2023-01-15', 'Activo'),
('Ara', 2, NULL, 'arasanpedro@yopmail.com', '2025-02-20', 'Inactivo'),
('Dollar city', 1, '3222222222', NULL, '2024-03-10', 'Activo'),
('D1', 3, '3333333333', 'd1andalucia@yopmail.com', '2023-04-05', 'Activo'),
('Olimpica', 4, NULL, 'olimpicabuga@yopmail.com', '2021-05-12', 'Inactivo');
GO

INSERT INTO Establecimiento (NombreEstablecimiento, Ingresos, NumeroEmpleados, ComercianteID)
VALUES
('Exito C.C la 14', 1500000.50, 30, 1), --1
('Exito C.C La herradura', 2500000.75, 25, 1),--2
('Ara del Alvernia', 980000.00, 11, 2),
('Ara de la Carrera 30', 9000.00, 12, 2),
('Ara del lago chilicote', 450000.25,13, 2),
('Dollar City Alvernia', 750000.60, 4, 3),
('Dollar City C.C la 14', 3200000.00, 10, 3),
('D1 el victoria', 1850000.40, 6, 4),
('D1 zona rosa', 620000.80, 3, 4),
('Olimpica del centro', 890000.90, 5, 5);
GO

-- RETO 04: Procedimiento almacenado
USE ComercioCoreDB;
GO

CREATE OR ALTER PROCEDURE ObtenerComerciantesActivos
AS
BEGIN
    SELECT
        c.RazonSocial,
        m.Nombre AS Municipio,
        c.Telefono,
        COALESCE(c.CorreoElectronico, '') AS CorreoElectronico,
        c.FechaRegistro,
        c.Estado,
        COUNT(e.ID) AS CantidadEstablecimientos,
        COALESCE(SUM(e.Ingresos), 0) AS TotalIngresos,
        COALESCE(SUM(e.NumeroEmpleados), 0) AS CantidadEmpleados
    FROM Comerciante c
    LEFT JOIN Establecimiento e ON c.ID = e.ComercianteID
    INNER JOIN Municipio m ON c.MunicipioId = m.ID
    WHERE c.Estado = 'Activo'
    GROUP BY
        c.RazonSocial,
        m.Nombre,
        c.Telefono,
        c.CorreoElectronico,
        c.FechaRegistro,
        c.Estado
    ORDER BY CantidadEstablecimientos DESC;
END;
GO


USE ComercioCoreDB
EXEC ObtenerComerciantesActivos;
GO