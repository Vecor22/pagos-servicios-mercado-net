-- ============================================================2026
-- SISTEMA DE ADMINISTRACIÓN DE MERCADO
-- Script Optimizado para SQL Server
-- ============================================================

-- 1. CREACIÓN DE LA BASE DE DATOS
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Mercado261')
    CREATE DATABASE Mercado261;
GO

USE Mercado261;
GO

-- ============================================================
-- 2. CREACIÓN DE TABLAS (DDL)
-- ============================================================

-- TABLA SOCIO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Socio')
BEGIN
    CREATE TABLE Socio (
        idSocio   INT IDENTITY(1,1) PRIMARY KEY,
        nombre    NVARCHAR(100) NOT NULL,
        dni       NVARCHAR(20)  NOT NULL,
        telefono  NVARCHAR(20)  NULL
    );
END
GO

-- TABLA PUESTO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Puesto')
BEGIN
    CREATE TABLE Puesto (
        idPuesto  INT IDENTITY(1,1) PRIMARY KEY,
        codigo    NVARCHAR(20)  NOT NULL,
        sector    NVARCHAR(50)  NULL,
        idSocio   INT           NULL,
        CONSTRAINT FK_Puesto_Socio FOREIGN KEY (idSocio) REFERENCES Socio(idSocio)
    );
END
GO

-- TABLA PAGO ALQUILER
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PagoAlquiler')
BEGIN
    CREATE TABLE PagoAlquiler (
        idPago    INT IDENTITY(1,1) PRIMARY KEY,
        idPuesto  INT           NOT NULL,
        fechaPago DATE          NOT NULL,
        monto     DECIMAL(10,2) NOT NULL,
        mes       NVARCHAR(20)  NOT NULL,
        anio      INT           NOT NULL,
        estado    NVARCHAR(20)  NOT NULL DEFAULT 'Pagado',
        CONSTRAINT FK_Pago_Puesto FOREIGN KEY (idPuesto) REFERENCES Puesto(idPuesto)
    );
END
GO

-- TABLA MANTENIMIENTO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Mantenimiento')
BEGIN
    CREATE TABLE Mantenimiento (
        idMantenimiento INT IDENTITY(1,1) PRIMARY KEY,
        tipo            NVARCHAR(50)  NOT NULL, -- Luz, Agua, Vigilancia, Limpieza, Internet, Otro
        descripcion     NVARCHAR(200) NOT NULL,
        fecha           DATE          NOT NULL,
        monto           DECIMAL(10,2) NOT NULL,
        proveedor       NVARCHAR(100) NULL,
        mes             NVARCHAR(20)  NOT NULL,
        anio            INT           NOT NULL
    );
END
GO

-- ============================================================
-- 3. PROCEDIMIENTOS ALMACENADOS (DML)
-- ============================================================

------------------------------------------------------------
-- ENTIDAD: SOCIO
------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_ListarSocio AS
BEGIN
    SELECT idSocio, nombre, dni, telefono FROM Socio ORDER BY nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarSocio @idSocio INT AS
BEGIN
    SELECT idSocio, nombre, dni, telefono FROM Socio WHERE idSocio = @idSocio;
END
GO

CREATE OR ALTER PROCEDURE sp_GuardarSocio
    @nombre    NVARCHAR(100),
    @dni       NVARCHAR(20),
    @telefono  NVARCHAR(20),
    @nuevoID   INT OUTPUT
AS
BEGIN
    INSERT INTO Socio (nombre, dni, telefono) VALUES (@nombre, @dni, @telefono);
    SET @nuevoID = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_ActualizarSocio
    @idSocio   INT,
    @nombre    NVARCHAR(100),
    @dni       NVARCHAR(20),
    @telefono  NVARCHAR(20),
    @procesado BIT OUTPUT
AS
BEGIN
    UPDATE Socio SET nombre=@nombre, dni=@dni, telefono=@telefono WHERE idSocio=@idSocio;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarSocio
    @idSocio   INT,
    @procesado BIT OUTPUT
AS
BEGIN
    DELETE FROM Socio WHERE idSocio = @idSocio;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

------------------------------------------------------------
-- ENTIDAD: PUESTO
------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_ListarPuesto AS
BEGIN
    SELECT p.idPuesto, p.codigo, p.sector,
           ISNULL(s.nombre, 'Sin socio') AS nombreSocio,
           p.idSocio
    FROM Puesto p
    LEFT JOIN Socio s ON s.idSocio = p.idSocio
    ORDER BY p.codigo;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarPuesto @idPuesto INT AS
BEGIN
    SELECT p.idPuesto, p.codigo, p.sector, p.idSocio,
           ISNULL(s.nombre,'Sin socio') AS nombreSocio
    FROM Puesto p
    LEFT JOIN Socio s ON s.idSocio = p.idSocio
    WHERE p.idPuesto = @idPuesto;
END
GO

CREATE OR ALTER PROCEDURE sp_GuardarPuesto
    @codigo   NVARCHAR(20),
    @sector   NVARCHAR(50),
    @idSocio  INT,
    @nuevoID  INT OUTPUT
AS
BEGIN
    INSERT INTO Puesto (codigo, sector, idSocio) VALUES (@codigo, @sector, @idSocio);
    SET @nuevoID = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_ActualizarPuesto
    @idPuesto  INT,
    @codigo    NVARCHAR(20),
    @sector    NVARCHAR(50),
    @idSocio   INT,
    @procesado BIT OUTPUT
AS
BEGIN
    UPDATE Puesto SET codigo=@codigo, sector=@sector, idSocio=@idSocio WHERE idPuesto=@idPuesto;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarPuesto
    @idPuesto  INT,
    @procesado BIT OUTPUT
AS
BEGIN
    DELETE FROM Puesto WHERE idPuesto = @idPuesto;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

------------------------------------------------------------
-- ENTIDAD: PAGO ALQUILER
------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_ListarPagoAlquiler AS
BEGIN
    SELECT pa.idPago, pa.idPuesto,
           p.codigo   AS codigoPuesto,
           ISNULL(s.nombre,'Sin socio') AS nombreSocio,
           pa.fechaPago, pa.monto, pa.mes, pa.anio, pa.estado
    FROM PagoAlquiler pa
    INNER JOIN Puesto p  ON pa.idPuesto = p.idPuesto
    LEFT  JOIN Socio  s  ON p.idSocio   = s.idSocio
    ORDER BY pa.anio DESC, pa.fechaPago DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarPagoAlquiler @idPago INT AS
BEGIN
    SELECT pa.idPago, pa.idPuesto,
           p.codigo   AS codigoPuesto,
           ISNULL(s.nombre,'Sin socio') AS nombreSocio,
           pa.fechaPago, pa.monto, pa.mes, pa.anio, pa.estado
    FROM PagoAlquiler pa
    INNER JOIN Puesto p  ON pa.idPuesto = p.idPuesto
    LEFT  JOIN Socio  s  ON p.idSocio   = s.idSocio
    WHERE pa.idPago = @idPago;
END
GO

CREATE OR ALTER PROCEDURE sp_GuardarPagoAlquiler
    @idPuesto  INT,
    @fechaPago DATE,
    @monto     DECIMAL(10,2),
    @mes       NVARCHAR(20),
    @anio      INT,
    @estado    NVARCHAR(20),
    @nuevoID   INT OUTPUT
AS
BEGIN
    INSERT INTO PagoAlquiler (idPuesto, fechaPago, monto, mes, anio, estado)
    VALUES (@idPuesto, @fechaPago, @monto, @mes, @anio, @estado);
    SET @nuevoID = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_ActualizarPagoAlquiler
    @idPago    INT,
    @idPuesto  INT,
    @fechaPago DATE,
    @monto     DECIMAL(10,2),
    @mes       NVARCHAR(20),
    @anio      INT,
    @estado    NVARCHAR(20),
    @procesado BIT OUTPUT
AS
BEGIN
    UPDATE PagoAlquiler
    SET idPuesto=@idPuesto, fechaPago=@fechaPago, monto=@monto,
        mes=@mes, anio=@anio, estado=@estado
    WHERE idPago = @idPago;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarPagoAlquiler
    @idPago    INT,
    @procesado BIT OUTPUT
AS
BEGIN
    DELETE FROM PagoAlquiler WHERE idPago = @idPago;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_ReportePagosPorAnio @anio INT AS
BEGIN
    SELECT pa.idPago, pa.idPuesto,
           p.codigo   AS codigoPuesto,
           ISNULL(s.nombre,'Sin socio') AS nombreSocio,
           pa.fechaPago, pa.monto, pa.mes, pa.anio, pa.estado
    FROM PagoAlquiler pa
    INNER JOIN Puesto p ON pa.idPuesto = p.idPuesto
    LEFT  JOIN Socio  s ON p.idSocio   = s.idSocio
    WHERE pa.anio = @anio
    ORDER BY pa.mes, pa.fechaPago;
END
GO

------------------------------------------------------------
-- ENTIDAD: MANTENIMIENTO
------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_ListarMantenimiento AS
BEGIN
    SELECT idMantenimiento, tipo, descripcion, fecha,
           monto, proveedor, mes, anio
    FROM Mantenimiento
    ORDER BY anio DESC, fecha DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarMantenimiento @idMantenimiento INT AS
BEGIN
    SELECT idMantenimiento, tipo, descripcion, fecha,
           monto, proveedor, mes, anio
    FROM Mantenimiento
    WHERE idMantenimiento = @idMantenimiento;
END
GO

CREATE OR ALTER PROCEDURE sp_GuardarMantenimiento
    @tipo         NVARCHAR(50),
    @descripcion  NVARCHAR(200),
    @fecha        DATE,
    @monto        DECIMAL(10,2),
    @proveedor    NVARCHAR(100),
    @mes          NVARCHAR(20),
    @anio         INT,
    @nuevoID      INT OUTPUT
AS
BEGIN
    INSERT INTO Mantenimiento (tipo, descripcion, fecha, monto, proveedor, mes, anio)
    VALUES (@tipo, @descripcion, @fecha, @monto, @proveedor, @mes, @anio);
    SET @nuevoID = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_ActualizarMantenimiento
    @idMantenimiento INT,
    @tipo            NVARCHAR(50),
    @descripcion     NVARCHAR(200),
    @fecha           DATE,
    @monto           DECIMAL(10,2),
    @proveedor       NVARCHAR(100),
    @mes             NVARCHAR(20),
    @anio            INT,
    @procesado       BIT OUTPUT
AS
BEGIN
    UPDATE Mantenimiento
    SET tipo=@tipo, descripcion=@descripcion, fecha=@fecha,
        monto=@monto, proveedor=@proveedor, mes=@mes, anio=@anio
    WHERE idMantenimiento = @idMantenimiento;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarMantenimiento
    @idMantenimiento INT,
    @procesado       BIT OUTPUT
AS
BEGIN
    DELETE FROM Mantenimiento WHERE idMantenimiento = @idMantenimiento;
    SET @procesado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

CREATE OR ALTER PROCEDURE sp_ReporteMantenimientoPorAnio @anio INT AS
BEGIN
    SELECT idMantenimiento, tipo, descripcion, fecha,
           monto, proveedor, mes, anio
    FROM Mantenimiento
    WHERE anio = @anio
    ORDER BY tipo, fecha;
END
GO

-- ============================================================
-- 4. CARGA DE DATOS DE PRUEBA
-- ============================================================

-- Datos: Socio
IF NOT EXISTS (SELECT 1 FROM Socio)
BEGIN
    INSERT INTO Socio (nombre, dni, telefono) VALUES
        ('Juan Perez',    '12345678', '999111222'),
        ('Maria Lopez',   '87654321', '999333444'),
        ('Carlos Ruiz',   '11223344', '999555666'),
        ('Ana Torres',    '44332211', '999777888');
END
GO

-- Datos: Puesto
IF NOT EXISTS (SELECT 1 FROM Puesto)
BEGIN
    INSERT INTO Puesto (codigo, sector, idSocio) VALUES
        ('A-01', 'Abarrotes',  1),
        ('A-02', 'Verduras',   2),
        ('B-01', 'Carnes',     3),
        ('B-02', 'Lacteos',    NULL),
        ('C-01', 'Ropa',       4);
END
GO

-- Datos: PagoAlquiler
IF NOT EXISTS (SELECT 1 FROM PagoAlquiler)
BEGIN
    INSERT INTO PagoAlquiler (idPuesto, fechaPago, monto, mes, anio, estado) VALUES
    (1, '2026-01-05', 250.00, 'Enero',    2026, 'Pagado'),
    (2, '2026-01-06', 300.00, 'Enero',    2026, 'Pagado'),
    (3, '2026-01-07', 280.00, 'Enero',    2026, 'Pagado'),
    (5, '2026-01-08', 200.00, 'Enero',    2026, 'Pendiente'),
    (1, '2026-02-05', 250.00, 'Febrero',  2026, 'Pagado'),
    (2, '2026-02-06', 300.00, 'Febrero',  2026, 'Pagado'),
    (3, '2026-02-10', 280.00, 'Febrero',  2026, 'Pendiente');
END
GO

-- Datos: Mantenimiento
IF NOT EXISTS (SELECT 1 FROM Mantenimiento)
BEGIN
    INSERT INTO Mantenimiento (tipo, descripcion, fecha, monto, proveedor, mes, anio) VALUES
    ('Luz',        'Factura electrica Enero',      '2026-01-10', 480.00,  'Enel',      'Enero',    2026),
    ('Agua',       'Recibo de agua Enero',          '2026-01-12', 120.00,  'Sedapal',   'Enero',    2026),
    ('Vigilancia', 'Servicio seguridad Enero',     '2026-01-15', 800.00,  'VigPeru',   'Enero',    2026),
    ('Limpieza',   'Limpieza areas comunes Enero', '2026-01-20', 250.00,  'LimpMax',   'Enero',    2026);
END
GO

PRINT '>>> Base de datos Mercado26 configurada íntegramente.';
GO