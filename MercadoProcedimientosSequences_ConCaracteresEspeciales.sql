USE MercadoDB;
GO

/* =========================================================
   ROL - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE OR ALTER PROCEDURE usp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rol_id,
        nombre,
        descripcion,
        activo,
        fecha_creacion,
        fecha_actualizacion
    FROM Rol
    ORDER BY nombre;
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_BuscarPorId
    @rol_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rol_id,
        nombre,
        descripcion,
        activo,
        fecha_creacion,
        fecha_actualizacion
    FROM Rol
    WHERE rol_id = @rol_id;
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_Crear
    @nombre VARCHAR(50),
    @descripcion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Rol (
        nombre,
        descripcion,
        activo,
        fecha_creacion
    )
    VALUES (
        @nombre,
        @descripcion,
        1,
        SYSDATETIME()
    );
END;
GO

CREATE OR ALTER PROCEDURE usp_Rol_Actualizar
    @rol_id BIGINT,
    @nombre VARCHAR(50),
    @descripcion VARCHAR(255) = NULL,
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

/* =========================================================
   ROL - DATO INICIAL
========================================================= */

IF NOT EXISTS (
    SELECT 1 FROM Rol WHERE nombre = 'ADMINISTRADOR'
)
BEGIN
    INSERT INTO Rol (
        nombre,
        descripcion,
        activo,
        fecha_creacion
    )
    VALUES (
        'ADMINISTRADOR',
        'Rol principal con acceso completo al sistema',
        1,
        SYSDATETIME()
    );
END;
GO


USE MercadoDB;
GO

/* =========================================================
   USUARIO - PROCEDIMIENTOS ALMACENADOS
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
        u.fecha_actualizacion,
        r.rol_id,
        r.nombre AS rol
    FROM Usuario u
    INNER JOIN Rol r ON u.rol_id = r.rol_id
    ORDER BY u.fecha_creacion DESC;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_BuscarPorId
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.usuario_id,
        u.username,
        u.password_hash,
        u.nombre_completo,
        u.foto_url,
        u.estado,
        u.fecha_creacion,
        u.fecha_actualizacion,
        r.rol_id,
        r.nombre AS rol
    FROM Usuario u
    INNER JOIN Rol r ON u.rol_id = r.rol_id
    WHERE u.usuario_id = @usuario_id;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_BuscarPorUsername
    @username VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.usuario_id,
        u.username,
        u.password_hash,
        u.nombre_completo,
        u.foto_url,
        u.estado,
        u.fecha_creacion,
        u.fecha_actualizacion,
        r.rol_id,
        r.nombre AS rol
    FROM Usuario u
    INNER JOIN Rol r ON u.rol_id = r.rol_id
    WHERE u.username = @username;
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_Crear
    @username VARCHAR(50),
    @password_hash VARCHAR(255),
    @nombre_completo VARCHAR(150),
    @foto_url VARCHAR(255) = NULL,
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
        fecha_creacion,
        rol_id
    )
    VALUES (
        @username,
        @password_hash,
        @nombre_completo,
        @foto_url,
        'ACTIVO',
        SYSDATETIME(),
        @rol_id
    );
END;
GO

CREATE OR ALTER PROCEDURE usp_Usuario_Actualizar
    @usuario_id BIGINT,
    @nombre_completo VARCHAR(150),
    @foto_url VARCHAR(255) = NULL,
    @rol_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET
        nombre_completo = @nombre_completo,
        foto_url = @foto_url,
        rol_id = @rol_id,
        fecha_actualizacion = SYSDATETIME()
    WHERE usuario_id = @usuario_id;
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
   AUTH - LOGIN
========================================================= */

CREATE OR ALTER PROCEDURE usp_Auth_Login
    @username VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.usuario_id,
        u.username,
        u.password_hash,
        u.nombre_completo,
        u.foto_url,
        u.estado,
        r.rol_id,
        r.nombre AS rol
    FROM Usuario u
    INNER JOIN Rol r ON u.rol_id = r.rol_id
    WHERE u.username = @username
      AND u.estado = 'ACTIVO'
      AND r.activo = 1;
END;
GO


/* =========================================================
   USUARIO - DATO INICIAL
   Nota: reemplaza el password_hash por el hash real generado en C#
========================================================= */

IF NOT EXISTS (
    SELECT 1 FROM Usuario WHERE username = 'admin'
)
BEGIN
    INSERT INTO Usuario (
        username,
        password_hash,
        nombre_completo,
        foto_url,
        estado,
        fecha_creacion,
        rol_id
    )
    VALUES (
        'admin',
        'CAMBIAR_POR_HASH_REAL',
        'Administrador del Sistema',
        NULL,
        'ACTIVO',
        SYSDATETIME(),
        (SELECT rol_id FROM Rol WHERE nombre = 'ADMINISTRADOR')
    );
END;
GO


USE MercadoDB;
GO

/* =========================================================
   SOCIO - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE OR ALTER PROCEDURE usp_Socio_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        socio_id,
        codigo_socio,
        nombres,
        apellidos,
        dni,
        correo,
        telefono,
        estado,
        fecha_creacion,
        fecha_actualizacion
    FROM Socio
    ORDER BY fecha_creacion DESC;
END;
GO


CREATE OR ALTER PROCEDURE usp_Socio_BuscarPorId
    @socio_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        socio_id,
        codigo_socio,
        nombres,
        apellidos,
        dni,
        correo,
        telefono,
        estado,
        fecha_creacion,
        fecha_actualizacion
    FROM Socio
    WHERE socio_id = @socio_id;
END;
GO


CREATE OR ALTER PROCEDURE usp_Socio_BuscarPorNombre
    @nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        socio_id,
        codigo_socio,
        nombres,
        apellidos,
        dni,
        correo,
        telefono,
        estado,
        fecha_creacion,
        fecha_actualizacion
    FROM Socio
    WHERE nombres LIKE '%' + @nombre + '%'
       OR apellidos LIKE '%' + @nombre + '%';
END;
GO


CREATE OR ALTER PROCEDURE usp_Socio_BuscarPorDni
    @dni CHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        socio_id,
        codigo_socio,
        nombres,
        apellidos,
        dni,
        correo,
        telefono,
        estado,
        fecha_creacion,
        fecha_actualizacion
    FROM Socio
    WHERE dni = @dni;
END;
GO



--------------------- SEQUENCE ------------------------

IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'Seq_Socio')
BEGIN
    CREATE SEQUENCE Seq_Socio
    AS BIGINT
    START WITH 1
    INCREMENT BY 1;
END;
GO


----------------------------------------------------------------------



CREATE OR ALTER PROCEDURE usp_Socio_Crear
    @nombres VARCHAR(100),
    @apellidos VARCHAR(100),
    @dni CHAR(8),
    @correo VARCHAR(120) = NULL,
    @telefono VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @codigo_socio VARCHAR(20);

    SET @codigo_socio = 'SOC-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Socio AS VARCHAR(10)), 6);

    INSERT INTO Socio (
        codigo_socio,
        nombres,
        apellidos,
        dni,
        correo,
        telefono,
        estado,
        fecha_creacion
    )
    VALUES (
        @codigo_socio,
        @nombres,
        @apellidos,
        @dni,
        @correo,
        @telefono,
        'ACTIVO',
        SYSDATETIME()
    );
END;
GO



--------------------------------------------------------------------------------


CREATE OR ALTER PROCEDURE usp_Socio_Actualizar
    @socio_id BIGINT,
    @nombres VARCHAR(100),
    @apellidos VARCHAR(100),
    @correo VARCHAR(120) = NULL,
    @telefono VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Socio
    SET
        nombres = @nombres,
        apellidos = @apellidos,
        correo = @correo,
        telefono = @telefono,
        fecha_actualizacion = SYSDATETIME()
    WHERE socio_id = @socio_id;
END;
GO


CREATE OR ALTER PROCEDURE usp_Socio_CambiarEstado
    @socio_id BIGINT,
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Socio
    SET
        estado = @estado,
        fecha_actualizacion = SYSDATETIME()
    WHERE socio_id = @socio_id;

    -- 🔥 Regla: si se inactiva, se desasignan sus puestos
    IF @estado = 'INACTIVO'
    BEGIN
        DELETE FROM SocioPuesto
        WHERE socio_id = @socio_id;
    END
END;
GO



----------------------------------------------------------------------------------------------------------------------------------------------------------

USE MercadoDB;
GO

/* =========================================================
   PUESTO - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE OR ALTER PROCEDURE usp_Puesto_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        puesto_id,
        codigo_puesto,
        fecha_creacion
    FROM Puesto
    ORDER BY fecha_creacion DESC;
END;
GO


CREATE OR ALTER PROCEDURE usp_Puesto_BuscarPorId
    @puesto_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        puesto_id,
        codigo_puesto,
        fecha_creacion
    FROM Puesto
    WHERE puesto_id = @puesto_id;
END;
GO


CREATE OR ALTER PROCEDURE usp_Puesto_BuscarPorCodigo
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        puesto_id,
        codigo_puesto,
        fecha_creacion
    FROM Puesto
    WHERE codigo_puesto = @codigo_puesto;
END;
GO

------------------------------  SEQUENCE  ------------------------------------


IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'Seq_Puesto')
BEGIN
    CREATE SEQUENCE Seq_Puesto
    AS BIGINT
    START WITH 1
    INCREMENT BY 1;
END;
GO


--------------------------------------------------------------------


CREATE OR ALTER PROCEDURE usp_Puesto_Crear
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @codigo_puesto VARCHAR(20);

    SET @codigo_puesto = 'PUE-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Puesto AS VARCHAR(10)), 6);

    INSERT INTO Puesto (
        codigo_puesto,
        fecha_creacion
    )
    VALUES (
        @codigo_puesto,
        SYSDATETIME()
    );
END;
GO



-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

USE MercadoDB;
GO

/* =========================================================
   SOCIO_PUESTO - LISTAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_SocioPuesto_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        sp.socio_puesto_id,
        s.codigo_socio,
        s.nombres,
        s.apellidos,
        s.dni,
        p.codigo_puesto,
        u.nombre_completo AS asignado_por,
        sp.fecha_asignacion
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    INNER JOIN Puesto p ON sp.puesto_id = p.puesto_id
    INNER JOIN Usuario u ON sp.asignado_por_usuario_id = u.usuario_id
    ORDER BY p.codigo_puesto;
END;
GO

/* =========================================================
   SOCIO_PUESTO - CREAR ASIGNACIÓN
   Regla: socio debe estar ACTIVO y el puesto libre
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
    WHERE codigo_socio = @codigo_socio
      AND estado = 'ACTIVO';

    SELECT @puesto_id = puesto_id
    FROM Puesto
    WHERE codigo_puesto = @codigo_puesto;

    IF @socio_id IS NULL
    BEGIN
        RAISERROR('Socio inválido o inactivo.',16,1);
        RETURN;
    END

    IF @puesto_id IS NULL
    BEGIN
        RAISERROR('El puesto no existe.',16,1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SocioPuesto WHERE puesto_id = @puesto_id)
    BEGIN
        RAISERROR('El puesto ya está asignado.',16,1);
        RETURN;
    END

    INSERT INTO SocioPuesto (
        socio_id,
        puesto_id,
        asignado_por_usuario_id,
        fecha_asignacion
    )
    VALUES (
        @socio_id,
        @puesto_id,
        @usuario_id,
        SYSDATETIME()
    );
END;
GO

/* =========================================================
   SOCIO_PUESTO - REASIGNAR
   Cambia el socio de un puesto existente
========================================================= */
CREATE OR ALTER PROCEDURE usp_SocioPuesto_Reasignar
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
    WHERE codigo_socio = @codigo_socio
      AND estado = 'ACTIVO';

    SELECT @puesto_id = puesto_id
    FROM Puesto
    WHERE codigo_puesto = @codigo_puesto;

    IF @socio_id IS NULL
    BEGIN
        RAISERROR('Socio inválido o inactivo.',16,1);
        RETURN;
    END

    IF @puesto_id IS NULL
    BEGIN
        RAISERROR('El puesto no existe.',16,1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM SocioPuesto WHERE puesto_id = @puesto_id)
    BEGIN
        RAISERROR('El puesto no tiene asignación previa.',16,1);
        RETURN;
    END

    UPDATE SocioPuesto
    SET 
        socio_id = @socio_id,
        asignado_por_usuario_id = @usuario_id,
        fecha_asignacion = SYSDATETIME()
    WHERE puesto_id = @puesto_id;
END;
GO

/* =========================================================
   SOCIO_PUESTO - BUSCAR POR CÓDIGO PUESTO
   Devuelve el socio asignado a un puesto
========================================================= */
CREATE OR ALTER PROCEDURE usp_SocioPuesto_BuscarPorCodigoPuesto
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.codigo_socio,
        s.nombres,
        s.apellidos,
        s.dni,
        p.codigo_puesto,
        u.nombre_completo AS asignado_por,
        sp.fecha_asignacion
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    INNER JOIN Puesto p ON sp.puesto_id = p.puesto_id
    INNER JOIN Usuario u ON sp.asignado_por_usuario_id = u.usuario_id
    WHERE p.codigo_puesto = @codigo_puesto;
END;
GO

/* =========================================================
   SOCIO_PUESTO - LISTAR POR NOMBRE SOCIO
========================================================= */
CREATE OR ALTER PROCEDURE usp_SocioPuesto_ListarPorNombreSocio
    @nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.codigo_socio,
        s.nombres,
        s.apellidos,
        s.dni,
        p.codigo_puesto,
        u.nombre_completo AS asignado_por,
        sp.fecha_asignacion
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    INNER JOIN Puesto p ON sp.puesto_id = p.puesto_id
    INNER JOIN Usuario u ON sp.asignado_por_usuario_id = u.usuario_id
    WHERE s.nombres LIKE '%' + @nombre + '%'
       OR s.apellidos LIKE '%' + @nombre + '%';
END;
GO

/* =========================================================
   SOCIO_PUESTO - LISTAR POR DNI SOCIO
========================================================= */
CREATE OR ALTER PROCEDURE usp_SocioPuesto_ListarPorDniSocio
    @dni CHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.codigo_socio,
        s.nombres,
        s.apellidos,
        s.dni,
        p.codigo_puesto,
        u.nombre_completo AS asignado_por,
        sp.fecha_asignacion
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    INNER JOIN Puesto p ON sp.puesto_id = p.puesto_id
    INNER JOIN Usuario u ON sp.asignado_por_usuario_id = u.usuario_id
    WHERE s.dni = @dni;
END;
GO


------------------------------------------------------------------------------------------------------------------------------------




USE MercadoDB;
GO

/* =========================================================
   CONCEPTO COBRO - LISTAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_ConceptoCobro_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        concepto_cobro_id,
        nombre,
        descripcion,
        tipo_cobro,
        fecha_creacion,
        fecha_actualizacion
    FROM ConceptoCobro
    ORDER BY nombre;
END;
GO

/* =========================================================
   CONCEPTO COBRO - BUSCAR POR ID
========================================================= */
CREATE OR ALTER PROCEDURE usp_ConceptoCobro_BuscarPorId
    @concepto_cobro_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        concepto_cobro_id,
        nombre,
        descripcion,
        tipo_cobro,
        fecha_creacion,
        fecha_actualizacion
    FROM ConceptoCobro
    WHERE concepto_cobro_id = @concepto_cobro_id;
END;
GO

/* =========================================================
   CONCEPTO COBRO - CREAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_ConceptoCobro_Crear
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255) = NULL,
    @tipo_cobro VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ConceptoCobro (
        nombre,
        descripcion,
        tipo_cobro,
        fecha_creacion
    )
    VALUES (
        @nombre,
        @descripcion,
        @tipo_cobro,
        SYSDATETIME()
    );
END;
GO

/* =========================================================
   CONCEPTO COBRO - ACTUALIZAR
   (no se elimina, solo se modifica)
========================================================= */
CREATE OR ALTER PROCEDURE usp_ConceptoCobro_Actualizar
    @concepto_cobro_id BIGINT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255) = NULL,
    @tipo_cobro VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ConceptoCobro
    SET
        nombre = @nombre,
        descripcion = @descripcion,
        tipo_cobro = @tipo_cobro,
        fecha_actualizacion = SYSDATETIME()
    WHERE concepto_cobro_id = @concepto_cobro_id;
END;
GO



------------------------------------------------------------------------------------------------------------------------------------------------------------



USE MercadoDB;
GO

/* =========================================================
   SECUENCIA (si no existe)
========================================================= */
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'Seq_Deuda')
BEGIN
    CREATE SEQUENCE Seq_Deuda
    AS BIGINT
    START WITH 1
    INCREMENT BY 1;
END
GO

/* =========================================================
   DEUDA - LISTAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_Deuda_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.deuda_id,
        d.codigo_deuda,
        d.monto,
        d.estado,
        d.tipo_generacion,
        d.observacion,
        d.fecha_generacion,
        cc.nombre AS concepto,
        p.codigo_puesto,
        CONCAT(s.nombres, ' ', s.apellidos) AS socio
    FROM Deuda d
    INNER JOIN ConceptoCobro cc ON d.concepto_cobro_id = cc.concepto_cobro_id
    LEFT JOIN Puesto p ON d.puesto_id = p.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    ORDER BY d.fecha_generacion DESC;
END;
GO

/* =========================================================
   DEUDA - FILTROS
========================================================= */
CREATE OR ALTER PROCEDURE usp_Deuda_ListarPorEstado
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Deuda
    WHERE estado = @estado;
END;
GO

CREATE OR ALTER PROCEDURE usp_Deuda_ListarPorFecha
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Deuda
    WHERE CAST(fecha_generacion AS DATE) BETWEEN @fecha_inicio AND @fecha_fin;
END;
GO

CREATE OR ALTER PROCEDURE usp_Deuda_ListarPorCodigoPuesto
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT d.*
    FROM Deuda d
    INNER JOIN Puesto p ON d.puesto_id = p.puesto_id
    WHERE p.codigo_puesto = @codigo_puesto;
END;
GO




/* =========================================================
   GENERAR DEUDAS HIJAS
========================================================= */


CREATE OR ALTER PROCEDURE usp_Deuda_GenerarHijasDistribuidas
    @deuda_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @monto DECIMAL(10,2);
    DECLARE @concepto_cobro_id BIGINT;
    DECLARE @observacion VARCHAR(255);
    DECLARE @usuario_id BIGINT;

    SELECT 
        @monto = monto,
        @concepto_cobro_id = concepto_cobro_id,
        @observacion = observacion,
        @usuario_id = creado_por_usuario_id
    FROM Deuda
    WHERE deuda_id = @deuda_id;

    DECLARE @cantidad INT;

    SELECT @cantidad = COUNT(*)
    FROM SocioPuesto sp
    INNER JOIN Socio s ON sp.socio_id = s.socio_id
    WHERE s.estado = 'ACTIVO';

    IF @cantidad = 0
    BEGIN
        RAISERROR('No hay socios activos para distribuir la deuda.', 16, 1);
        RETURN;
    END

    DECLARE @base DECIMAL(10,2) = ROUND(@monto / @cantidad, 2);
    DECLARE @acumulado DECIMAL(10,2) = 0;
    DECLARE @contador INT = 1;

    DECLARE @puesto_id BIGINT;
    DECLARE @socio_id BIGINT;
    DECLARE @monto_hijo DECIMAL(10,2);
    DECLARE @codigo_deuda VARCHAR(20);

    DECLARE cur CURSOR FOR
        SELECT sp.puesto_id, sp.socio_id
        FROM SocioPuesto sp
        INNER JOIN Socio s ON sp.socio_id = s.socio_id
        WHERE s.estado = 'ACTIVO'
        ORDER BY sp.socio_puesto_id;

    OPEN cur;

    FETCH NEXT FROM cur INTO @puesto_id, @socio_id;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @contador = @cantidad
            SET @monto_hijo = @monto - @acumulado;
        ELSE
            SET @monto_hijo = @base;

        SET @codigo_deuda = 'DEU-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Deuda AS VARCHAR(10)), 6);

        INSERT INTO Deuda (
            codigo_deuda,
            monto,
            estado,
            tipo_generacion,
            observacion,
            concepto_cobro_id,
            puesto_id,
            socio_id,
            deuda_origen_id,
            creado_por_usuario_id
        )
        VALUES (
            @codigo_deuda,
            @monto_hijo,
            'PENDIENTE',
            'INDIVIDUAL',
            @observacion,
            @concepto_cobro_id,
            @puesto_id,
            @socio_id,
            @deuda_id,
            @usuario_id
        );

        SET @acumulado = @acumulado + @monto_hijo;
        SET @contador = @contador + 1;

        FETCH NEXT FROM cur INTO @puesto_id, @socio_id;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;
GO



/* =========================================================
   DEUDA - CREAR
========================================================= */


CREATE OR ALTER PROCEDURE usp_Deuda_Crear
    @concepto_cobro_id BIGINT,
    @codigo_puesto VARCHAR(20) = NULL,
    @monto DECIMAL(10,2),
    @tipo_generacion VARCHAR(20),
    @observacion VARCHAR(255) = NULL,
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @monto < 1
        BEGIN
            RAISERROR('Monto mínimo 1.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        DECLARE @codigo_deuda VARCHAR(20);
        SET @codigo_deuda = 'DEU-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Deuda AS VARCHAR(10)), 6);

        DECLARE @puesto_id BIGINT;
        DECLARE @socio_id BIGINT;

        IF @tipo_generacion = 'REPARTIBLE'
        BEGIN
            INSERT INTO Deuda (
                codigo_deuda,
                monto,
                estado,
                tipo_generacion,
                observacion,
                concepto_cobro_id,
                creado_por_usuario_id
            )
            VALUES (
                @codigo_deuda,
                @monto,
                'DISTRIBUIDA',
                'REPARTIBLE',
                @observacion,
                @concepto_cobro_id,
                @usuario_id
            );

            DECLARE @deuda_id BIGINT = SCOPE_IDENTITY();

            EXEC usp_Deuda_GenerarHijasDistribuidas @deuda_id;
        END
        ELSE IF @tipo_generacion = 'INDIVIDUAL'
        BEGIN
            SELECT @puesto_id = puesto_id
            FROM Puesto
            WHERE codigo_puesto = @codigo_puesto;

            IF @puesto_id IS NULL
            BEGIN
                RAISERROR('El puesto no existe.', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END

            SELECT @socio_id = sp.socio_id
            FROM SocioPuesto sp
            INNER JOIN Socio s ON sp.socio_id = s.socio_id
            WHERE sp.puesto_id = @puesto_id
              AND s.estado = 'ACTIVO';

            IF @socio_id IS NULL
            BEGIN
                INSERT INTO Deuda (
                    codigo_deuda,
                    monto,
                    estado,
                    tipo_generacion,
                    observacion,
                    concepto_cobro_id,
                    puesto_id,
                    creado_por_usuario_id
                )
                VALUES (
                    @codigo_deuda,
                    @monto,
                    'DISTRIBUIDA',
                    'INDIVIDUAL',
                    @observacion,
                    @concepto_cobro_id,
                    @puesto_id,
                    @usuario_id
                );

                DECLARE @deuda_id2 BIGINT = SCOPE_IDENTITY();

                EXEC usp_Deuda_GenerarHijasDistribuidas @deuda_id2;
            END
            ELSE
            BEGIN
                INSERT INTO Deuda (
                    codigo_deuda,
                    monto,
                    estado,
                    tipo_generacion,
                    observacion,
                    concepto_cobro_id,
                    puesto_id,
                    socio_id,
                    creado_por_usuario_id
                )
                VALUES (
                    @codigo_deuda,
                    @monto,
                    'PENDIENTE',
                    'INDIVIDUAL',
                    @observacion,
                    @concepto_cobro_id,
                    @puesto_id,
                    @socio_id,
                    @usuario_id
                );
            END
        END
        ELSE
        BEGIN
            RAISERROR('Tipo de generación inválido.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;
GO


/* =========================================================
   EXONERAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_Deuda_Exonerar
    @deuda_id BIGINT,
    @motivo VARCHAR(255),
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Deuda
    SET 
        estado = 'EXONERADA',
        fecha_exoneracion = SYSDATETIME(),
        motivo_exoneracion = @motivo,
        exonerado_por_usuario_id = @usuario_id
    WHERE deuda_id = @deuda_id
      AND estado = 'PENDIENTE';
END;
GO





USE MercadoDB;
GO

/* =========================================================
   DEUDA - BUSCAR POR ID
========================================================= */
CREATE OR ALTER PROCEDURE usp_Deuda_BuscarPorId
    @deuda_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.deuda_id,
        d.codigo_deuda,
        d.monto,
        d.estado,
        d.tipo_generacion,
        d.observacion,
        d.fecha_generacion,
        d.fecha_exoneracion,
        d.motivo_exoneracion,
        cc.nombre AS concepto,
        p.codigo_puesto,
        CONCAT(s.nombres, ' ', s.apellidos) AS socio
    FROM Deuda d
    INNER JOIN ConceptoCobro cc ON d.concepto_cobro_id = cc.concepto_cobro_id
    LEFT JOIN Puesto p ON d.puesto_id = p.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    WHERE d.deuda_id = @deuda_id;
END;
GO

/* =========================================================
   DEUDA - BUSCAR POR CÓDIGO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Deuda_BuscarPorCodigo
    @codigo_deuda VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.deuda_id,
        d.codigo_deuda,
        d.monto,
        d.estado,
        d.tipo_generacion,
        d.observacion,
        d.fecha_generacion,
        d.fecha_exoneracion,
        d.motivo_exoneracion,
        cc.nombre AS concepto,
        p.codigo_puesto,
        CONCAT(s.nombres, ' ', s.apellidos) AS socio
    FROM Deuda d
    INNER JOIN ConceptoCobro cc ON d.concepto_cobro_id = cc.concepto_cobro_id
    LEFT JOIN Puesto p ON d.puesto_id = p.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    WHERE d.codigo_deuda = @codigo_deuda;
END;
GO


-----------------------------------------------------------------------------------------------------------------------------------------------------------



USE MercadoDB;
GO

/* =========================================================
   SECUENCIA (si no existe)
========================================================= */
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'Seq_Pago')
BEGIN
    CREATE SEQUENCE Seq_Pago
    AS BIGINT
    START WITH 1
    INCREMENT BY 1;
END
GO

/* =========================================================
   PAGO - LISTAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.pago_id,
        p.codigo_pago,
        d.codigo_deuda,
        pu.codigo_puesto,
        CONCAT(s.nombres,' ',s.apellidos) AS socio,
        p.monto_pagado,
        p.medio_pago,
        p.numero_operacion,
        p.estado,
        p.fecha_pago
    FROM Pago p
    INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
    LEFT JOIN Puesto pu ON d.puesto_id = pu.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    ORDER BY p.fecha_pago DESC;
END;
GO

/* =========================================================
   PAGO - BUSCAR POR ID
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_BuscarPorId
    @pago_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Pago WHERE pago_id = @pago_id;
END;
GO

/* =========================================================
   PAGO - BUSCAR POR CÓDIGO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_BuscarPorCodigo
    @codigo_pago VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Pago WHERE codigo_pago = @codigo_pago;
END;
GO

/* =========================================================
   PAGO - BUSCAR POR CÓDIGO DE DEUDA
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_BuscarPorCodigoDeuda
    @codigo_deuda VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.*
    FROM Pago p
    INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
    WHERE d.codigo_deuda = @codigo_deuda;
END;
GO

/* =========================================================
   PAGO - LISTAR POR PUESTO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_ListarPorCodigoPuesto
    @codigo_puesto VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.*
    FROM Pago p
    INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
    INNER JOIN Puesto pu ON d.puesto_id = pu.puesto_id
    WHERE pu.codigo_puesto = @codigo_puesto;
END;
GO

/* =========================================================
   PAGO - LISTAR POR FECHAS
========================================================= */
CREATE OR ALTER PROCEDURE usp_Pago_ListarPorFechas
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Pago
    WHERE CAST(fecha_pago AS DATE) BETWEEN @fecha_inicio AND @fecha_fin;
END;
GO

/* =========================================================
   PAGO - CREAR
   ✔ Solo deudas PENDIENTE
   ✔ Un pago por deuda
   ✔ Genera comprobante automático
========================================================= */

CREATE OR ALTER PROCEDURE usp_Pago_Crear
    @codigo_deuda VARCHAR(20),
    @medio_pago VARCHAR(30),
    @numero_operacion VARCHAR(50) = NULL,
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @deuda_id BIGINT;
        DECLARE @monto DECIMAL(10,2);

        SELECT 
            @deuda_id = deuda_id,
            @monto = monto
        FROM Deuda
        WHERE codigo_deuda = @codigo_deuda
          AND estado = 'PENDIENTE';

        IF @deuda_id IS NULL
        BEGIN
            RAISERROR('Deuda no válida o no pendiente',16,1);
            ROLLBACK;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Pago WHERE deuda_id = @deuda_id AND estado = 'REGISTRADO')
        BEGIN
            RAISERROR('La deuda ya tiene pago',16,1);
            ROLLBACK;
            RETURN;
        END

        DECLARE @codigo_pago VARCHAR(20);
        SET @codigo_pago = 'PAG-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Pago AS VARCHAR(10)),6);

        INSERT INTO Pago (
            codigo_pago,monto_pagado,medio_pago,numero_operacion,
            estado,deuda_id,registrado_por_usuario_id,fecha_pago
        )
        VALUES (
            @codigo_pago,@monto,@medio_pago,@numero_operacion,
            'REGISTRADO',@deuda_id,@usuario_id,SYSDATETIME()
        );

        DECLARE @pago_id BIGINT = SCOPE_IDENTITY();

        UPDATE Deuda
        SET estado = 'PAGADA'
        WHERE deuda_id = @deuda_id;

        EXEC usp_Comprobante_Crear @pago_id, @usuario_id;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END;
GO

/* =========================================================
   PAGO - ANULAR
========================================================= */


CREATE OR ALTER PROCEDURE usp_Pago_Anular
    @pago_id BIGINT,
    @motivo VARCHAR(255),
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @deuda_id BIGINT;

        SELECT @deuda_id = deuda_id
        FROM Pago
        WHERE pago_id = @pago_id
          AND estado = 'REGISTRADO';

        IF @deuda_id IS NULL
        BEGIN
            RAISERROR('Pago inválido',16,1);
            ROLLBACK;
            RETURN;
        END

        UPDATE Pago
        SET 
            estado = 'ANULADO',
            fecha_anulacion = SYSDATETIME(),
            motivo_anulacion = @motivo,
            anulado_por_usuario_id = @usuario_id
        WHERE pago_id = @pago_id;

        UPDATE Deuda
        SET estado = 'PENDIENTE'
        WHERE deuda_id = @deuda_id;

        EXEC usp_Comprobante_AnularPorPago @pago_id, @motivo, @usuario_id;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END;
GO



-----------------------------------------------------------------------------------------------------------------------------------------------------



USE MercadoDB;
GO

/* =========================================================
   SECUENCIA COMPROBANTE
========================================================= */
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'Seq_Comprobante')
BEGIN
    CREATE SEQUENCE Seq_Comprobante
    AS BIGINT
    START WITH 1
    INCREMENT BY 1;
END
GO

/* =========================================================
   COMPROBANTE - CREAR
========================================================= */
CREATE OR ALTER PROCEDURE usp_Comprobante_Crear
    @pago_id BIGINT,
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @numero VARCHAR(20);

    SET @numero = 'BOL-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Comprobante AS VARCHAR(10)), 6);

    INSERT INTO Comprobante (
        numero_comprobante,
        tipo_comprobante,
        estado,
        fecha_emision,
        pago_id
    )
    VALUES (
        @numero,
        'BOLETA',
        'EMITIDO',
        SYSDATETIME(),
        @pago_id
    );
END;
GO

/* =========================================================
   COMPROBANTE - BUSCAR POR ID PAGO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Comprobante_BuscarPorIdPago
    @pago_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.comprobante_id,
        c.numero_comprobante,
        c.tipo_comprobante,
        c.estado,
        c.fecha_emision,
        c.fecha_anulacion,
        c.motivo_anulacion,
        p.codigo_pago,
        d.codigo_deuda,
        pu.codigo_puesto,
        CONCAT(s.nombres, ' ', s.apellidos) AS socio,
        p.monto_pagado,
        p.medio_pago,
        p.numero_operacion,
        p.fecha_pago,
        ur.nombre_completo AS registrado_por,
        ua.nombre_completo AS anulado_por
    FROM Comprobante c
    INNER JOIN Pago p ON c.pago_id = p.pago_id
    INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
    LEFT JOIN Puesto pu ON d.puesto_id = pu.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    INNER JOIN Usuario ur ON p.registrado_por_usuario_id = ur.usuario_id
    LEFT JOIN Usuario ua ON c.anulado_por_usuario_id = ua.usuario_id
    WHERE c.pago_id = @pago_id;
END;
GO

/* =========================================================
   COMPROBANTE - BUSCAR POR NÚMERO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Comprobante_BuscarPorNumero
    @numero_comprobante VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.comprobante_id,
        c.numero_comprobante,
        c.tipo_comprobante,
        c.estado,
        c.fecha_emision,
        c.fecha_anulacion,
        c.motivo_anulacion,
        p.codigo_pago,
        d.codigo_deuda,
        pu.codigo_puesto,
        CONCAT(s.nombres, ' ', s.apellidos) AS socio,
        p.monto_pagado,
        p.medio_pago,
        p.numero_operacion,
        p.fecha_pago
    FROM Comprobante c
    INNER JOIN Pago p ON c.pago_id = p.pago_id
    INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
    LEFT JOIN Puesto pu ON d.puesto_id = pu.puesto_id
    LEFT JOIN Socio s ON d.socio_id = s.socio_id
    WHERE c.numero_comprobante = @numero_comprobante;
END;
GO

/* =========================================================
   COMPROBANTE - ANULAR POR PAGO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Comprobante_AnularPorPago
    @pago_id BIGINT,
    @motivo VARCHAR(255),
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Comprobante
    SET
        estado = 'ANULADO',
        fecha_anulacion = SYSDATETIME(),
        motivo_anulacion = @motivo,
        anulado_por_usuario_id = @usuario_id
    WHERE pago_id = @pago_id
      AND estado = 'EMITIDO';
END;
GO



---------------------------------------------------------------------------------------------------------------------------------------------------------


USE MercadoDB;
GO

/* =========================================================
   REPORTE - RESUMEN INGRESOS DEL DÍA
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_ResumenIngresosDia
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(monto_pagado), 0) AS total_ingresos,
        COUNT(*) AS cantidad_pagos
    FROM Pago
    WHERE estado = 'REGISTRADO'
      AND CAST(fecha_pago AS DATE) = CAST(GETDATE() AS DATE);
END;
GO

/* =========================================================
   REPORTE - RESUMEN INGRESOS DEL MES
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_ResumenIngresosMes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(monto_pagado), 0) AS total_ingresos,
        COUNT(*) AS cantidad_pagos
    FROM Pago
    WHERE estado = 'REGISTRADO'
      AND MONTH(fecha_pago) = MONTH(GETDATE())
      AND YEAR(fecha_pago) = YEAR(GETDATE());
END;
GO

/* =========================================================
   REPORTE - RESUMEN INGRESOS DEL AÑO
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_ResumenIngresosAnio
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(monto_pagado), 0) AS total_ingresos,
        COUNT(*) AS cantidad_pagos
    FROM Pago
    WHERE estado = 'REGISTRADO'
      AND YEAR(fecha_pago) = YEAR(GETDATE());
END;
GO

/* =========================================================
   REPORTE - RESUMEN INGRESOS ENTRE FECHAS
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_ResumenIngresosEntreFechas
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(monto_pagado), 0) AS total_ingresos,
        COUNT(*) AS cantidad_pagos
    FROM Pago
    WHERE estado = 'REGISTRADO'
      AND CAST(fecha_pago AS DATE) BETWEEN @fecha_inicio AND @fecha_fin;
END;
GO

/* =========================================================
   REPORTE - RESUMEN DE DEUDAS
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_ResumenDeudas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ISNULL(SUM(monto), 0) AS total_deuda,
        ISNULL(SUM(CASE WHEN estado = 'PAGADA' THEN monto ELSE 0 END), 0) AS total_pagado,
        ISNULL(SUM(CASE WHEN estado = 'PENDIENTE' THEN monto ELSE 0 END), 0) AS total_pendiente,
        ISNULL(SUM(CASE WHEN estado = 'EXONERADA' THEN monto ELSE 0 END), 0) AS total_exonerado,
        ISNULL(SUM(CASE WHEN estado = 'PAGADA' THEN monto ELSE 0 END), 0) AS caja_actual,
        COUNT(*) AS total_deudas,
        SUM(CASE WHEN estado = 'PENDIENTE' THEN 1 ELSE 0 END) AS total_deudas_pagables
    FROM Deuda
    WHERE estado <> 'DISTRIBUIDA';
END;
GO

/* =========================================================
   REPORTE - DEUDAS POR ESTADO ENTRE FECHAS
========================================================= */
CREATE OR ALTER PROCEDURE usp_Reporte_DeudasPorEstadoEntreFechas
    @estado VARCHAR(20),
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        estado,
        COUNT(*) AS cantidad,
        ISNULL(SUM(monto), 0) AS total_monto,
        CAST(
            COUNT(*) * 100.0 / NULLIF(
                (
                    SELECT COUNT(*)
                    FROM Deuda
                    WHERE estado <> 'DISTRIBUIDA'
                      AND CAST(fecha_generacion AS DATE) BETWEEN @fecha_inicio AND @fecha_fin
                ),
                0
            ) 
            AS DECIMAL(10,2)
        ) AS porcentaje
    FROM Deuda
    WHERE estado = @estado
      AND estado <> 'DISTRIBUIDA'
      AND CAST(fecha_generacion AS DATE) BETWEEN @fecha_inicio AND @fecha_fin
    GROUP BY estado;
END;
GO


---------------------------------------------------------------------


CREATE OR ALTER PROCEDURE usp_Reporte_DeudasPorEstado
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        estado,
        COUNT(*) AS cantidad,
        ISNULL(SUM(monto), 0) AS total_monto,
        CAST(
            COUNT(*) * 100.0 / NULLIF((SELECT COUNT(*) FROM Deuda), 0)
            AS DECIMAL(10,2)
        ) AS porcentaje
    FROM Deuda
    WHERE estado = @estado
    GROUP BY estado;
END;
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------



------------- Realiza el hash del password del usuario administrador --------------------
UPDATE Usuario
SET username = 'admin01',
	password_hash = '042b42e94870ac30d9d5836c36e8a19f66539d3d79429d28f5eb508a9bbaf707'
WHERE usuario_id = '1';

----------------------------------------------------------------------------------------

------------------- Credenciales con password hash ---------------------
------- Se usará para validar el inicio de sesión como json, no ejecutar en sql server ------------
--{
--  "username": "admin01",
--  "password": "Admin01@"
--}
------------------------------------------------------------------------