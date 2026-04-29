USE MercadoDB;
GO

/* =========================================================
   ROL
========================================================= */

CREATE OR ALTER PROCEDURE usp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Rol;
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_Crear
    @nombre VARCHAR(50),
    @descripcion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Rol (nombre, descripcion, activo)
    VALUES (@nombre, @descripcion, 1);
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_Actualizar
    @rol_id BIGINT,
    @nombre VARCHAR(50),
    @descripcion VARCHAR(255),
    @activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Rol
    SET 
        nombre = @nombre,
        descripcion = @descripcion,
        activo = @activo,
        fecha_actualizacion = SYSDATETIME()
    WHERE rol_id = @rol_id;
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_BuscarPorId
    @rol_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Rol WHERE rol_id = @rol_id;
END;
GO


/* =========================================================
   USUARIO
========================================================= */

CREATE OR ALTER PROCEDURE usp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.usuario_id,
        u.username,
        u.nombre_completo,
        u.foto_url,
        u.estado,
        u.fecha_creacion,
        r.nombre AS rol
    FROM Usuario u
    INNER JOIN Rol r ON u.rol_id = r.rol_id;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_Crear
    @username VARCHAR(50),
    @password_hash VARCHAR(255),
    @nombre_completo VARCHAR(150),
    @foto_url VARCHAR(255),
    @rol_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuario (
        username,
        password_hash,
        nombre_completo,
        foto_url,
        estado,
        rol_id
    )
    VALUES (
        @username,
        @password_hash,
        @nombre_completo,
        @foto_url,
        'ACTIVO',
        @rol_id
    );
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_BuscarPorUsername
    @username VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Usuario WHERE username = @username;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_CambiarEstado
    @usuario_id BIGINT,
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET 
        estado = @estado,
        fecha_actualizacion = SYSDATETIME()
    WHERE usuario_id = @usuario_id;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_CambiarPassword
    @usuario_id BIGINT,
    @password_hash VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET 
        password_hash = @password_hash,
        fecha_actualizacion = SYSDATETIME()
    WHERE usuario_id = @usuario_id;
END;
GO


/* =========================================================
   SOCIO
========================================================= */

CREATE OR ALTER PROCEDURE usp_Socio_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Socio;
END;
GO

CREATE OR ALTER PROCEDURE usp_Socio_Crear
    @codigo_socio VARCHAR(20),
    @nombres VARCHAR(100),
    @apellidos VARCHAR(100),
    @dni CHAR(8),
    @correo VARCHAR(120),
    @telefono VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Socio (
        codigo_socio, nombres, apellidos, dni, correo, telefono, estado
    )
    VALUES (
        @codigo_socio, @nombres, @apellidos, @dni, @correo, @telefono, 'ACTIVO'
    );
END;
GO

CREATE OR ALTER PROCEDURE usp_Socio_CambiarEstado
    @socio_id BIGINT,
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Socio
    SET estado = @estado,
        fecha_actualizacion = SYSDATETIME()
    WHERE socio_id = @socio_id;

    -- 🔥 Regla: desasignar puestos si se inactiva
    IF @estado = 'INACTIVO'
    BEGIN
        DELETE FROM SocioPuesto
        WHERE socio_id = @socio_id;
    END
END;
GO


/* =========================================================
   PUESTO
========================================================= */

CREATE OR ALTER PROCEDURE usp_Puesto_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Puesto;
END;
GO

CREATE OR ALTER PROCEDURE usp_Puesto_Crear
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Puesto (codigo_puesto)
    VALUES (@codigo_puesto);
END;
GO


/* =========================================================
   SOCIO_PUESTO
========================================================= */

CREATE OR ALTER PROCEDURE usp_SocioPuesto_CrearAsignacion
    @codigo_socio VARCHAR(20),
    @codigo_puesto VARCHAR(20),
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @socio_id BIGINT;
    DECLARE @puesto_id BIGINT;

    SELECT @socio_id = socio_id
    FROM Socio
    WHERE codigo_socio = @codigo_socio AND estado = 'ACTIVO';

    SELECT @puesto_id = puesto_id
    FROM Puesto
    WHERE codigo_puesto = @codigo_puesto;

    IF @socio_id IS NULL
        RAISERROR('Socio inválido o inactivo',16,1);

    IF EXISTS (SELECT 1 FROM SocioPuesto WHERE puesto_id = @puesto_id)
        RAISERROR('El puesto ya está asignado',16,1);

    INSERT INTO SocioPuesto (
        socio_id, puesto_id, asignado_por_usuario_id
    )
    VALUES (
        @socio_id, @puesto_id, @usuario_id
    );
END;
GO

CREATE OR ALTER PROCEDURE usp_SocioPuesto_BuscarPorCodigoPuesto
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.nombres,
        s.apellidos,
        p.codigo_puesto
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    INNER JOIN Puesto p ON sp.puesto_id = p.puesto_id
    WHERE p.codigo_puesto = @codigo_puesto;
END;
GO


/* =========================================================
   CONCEPTO COBRO
========================================================= */

CREATE OR ALTER PROCEDURE usp_ConceptoCobro_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM ConceptoCobro;
END;
GO

CREATE OR ALTER PROCEDURE usp_ConceptoCobro_Crear
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255),
    @tipo_cobro VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ConceptoCobro (
        nombre, descripcion, tipo_cobro
    )
    VALUES (
        @nombre, @descripcion, @tipo_cobro
    );
END;
GO