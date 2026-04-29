CREATE DATABASE MercadoDB;
GO

USE MercadoDB;
GO

CREATE TABLE Rol (
    rol_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NULL,
    activo BIT NOT NULL DEFAULT 1,
    fecha_creacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_actualizacion DATETIME2 NULL
);

CREATE TABLE Usuario (
    usuario_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    nombre_completo VARCHAR(150) NOT NULL,
    foto_url VARCHAR(255) NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'ACTIVO',
    fecha_creacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_actualizacion DATETIME2 NULL,
    rol_id BIGINT NOT NULL,

    CONSTRAINT FK_Usuario_Rol 
        FOREIGN KEY (rol_id) REFERENCES Rol(rol_id),

    CONSTRAINT CK_Usuario_Estado 
        CHECK (estado IN ('ACTIVO', 'INACTIVO'))
);

CREATE TABLE Socio (
    socio_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo_socio VARCHAR(20) NOT NULL UNIQUE,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    dni CHAR(8) NOT NULL UNIQUE,
    correo VARCHAR(120) NULL,
    telefono VARCHAR(20) NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'ACTIVO',
    fecha_creacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_actualizacion DATETIME2 NULL,

    CONSTRAINT CK_Socio_Estado 
        CHECK (estado IN ('ACTIVO', 'INACTIVO')),

    CONSTRAINT CK_Socio_Dni 
        CHECK (dni LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

CREATE TABLE Puesto (
    puesto_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo_puesto VARCHAR(20) NOT NULL UNIQUE,
    fecha_creacion DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

CREATE TABLE SocioPuesto (
    socio_puesto_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    socio_id BIGINT NOT NULL,
    puesto_id BIGINT NOT NULL,
    asignado_por_usuario_id BIGINT NOT NULL,
    fecha_asignacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_SocioPuesto_Socio 
        FOREIGN KEY (socio_id) REFERENCES Socio(socio_id),

    CONSTRAINT FK_SocioPuesto_Puesto 
        FOREIGN KEY (puesto_id) REFERENCES Puesto(puesto_id),

    CONSTRAINT FK_SocioPuesto_Usuario 
        FOREIGN KEY (asignado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT UQ_SocioPuesto_Puesto 
        UNIQUE (puesto_id)
);

CREATE TABLE ConceptoCobro (
    concepto_cobro_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NULL,
    tipo_cobro VARCHAR(20) NOT NULL,
    fecha_creacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_actualizacion DATETIME2 NULL,

    CONSTRAINT CK_ConceptoCobro_Tipo 
        CHECK (tipo_cobro IN ('FIJO', 'CONSUMO'))
);

CREATE TABLE Deuda (
    deuda_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo_deuda VARCHAR(20) NOT NULL UNIQUE,
    monto DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',
    tipo_generacion VARCHAR(20) NOT NULL,
    observacion VARCHAR(255) NULL,
    fecha_generacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_exoneracion DATETIME2 NULL,
    motivo_exoneracion VARCHAR(255) NULL,

    concepto_cobro_id BIGINT NOT NULL,
    puesto_id BIGINT NULL,
    socio_id BIGINT NULL,
    deuda_origen_id BIGINT NULL,
    creado_por_usuario_id BIGINT NOT NULL,
    exonerado_por_usuario_id BIGINT NULL,

    CONSTRAINT FK_Deuda_ConceptoCobro 
        FOREIGN KEY (concepto_cobro_id) REFERENCES ConceptoCobro(concepto_cobro_id),

    CONSTRAINT FK_Deuda_Puesto 
        FOREIGN KEY (puesto_id) REFERENCES Puesto(puesto_id),

    CONSTRAINT FK_Deuda_Socio 
        FOREIGN KEY (socio_id) REFERENCES Socio(socio_id),

    CONSTRAINT FK_Deuda_DeudaOrigen 
        FOREIGN KEY (deuda_origen_id) REFERENCES Deuda(deuda_id),

    CONSTRAINT FK_Deuda_CreadoPor 
        FOREIGN KEY (creado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT FK_Deuda_ExoneradoPor 
        FOREIGN KEY (exonerado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT CK_Deuda_Estado 
        CHECK (estado IN ('PENDIENTE', 'PAGADA', 'EXONERADA', 'DISTRIBUIDA')),

    CONSTRAINT CK_Deuda_TipoGeneracion 
        CHECK (tipo_generacion IN ('INDIVIDUAL', 'REPARTIBLE')),

    CONSTRAINT CK_Deuda_Monto 
        CHECK (monto >= 1)
);

CREATE TABLE Pago (
    pago_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo_pago VARCHAR(20) NOT NULL UNIQUE,
    monto_pagado DECIMAL(10,2) NOT NULL,
    medio_pago VARCHAR(30) NOT NULL,
    numero_operacion VARCHAR(50) NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'REGISTRADO',
    fecha_pago DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_anulacion DATETIME2 NULL,
    motivo_anulacion VARCHAR(255) NULL,

    deuda_id BIGINT NOT NULL UNIQUE,
    registrado_por_usuario_id BIGINT NOT NULL,
    anulado_por_usuario_id BIGINT NULL,

    CONSTRAINT FK_Pago_Deuda 
        FOREIGN KEY (deuda_id) REFERENCES Deuda(deuda_id),

    CONSTRAINT FK_Pago_RegistradoPor 
        FOREIGN KEY (registrado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT FK_Pago_AnuladoPor 
        FOREIGN KEY (anulado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT CK_Pago_Estado 
        CHECK (estado IN ('REGISTRADO', 'ANULADO')),

    CONSTRAINT CK_Pago_Monto 
        CHECK (monto_pagado >= 1)
);

CREATE TABLE Comprobante (
    comprobante_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    numero_comprobante VARCHAR(20) NOT NULL UNIQUE,
    tipo_comprobante VARCHAR(20) NOT NULL DEFAULT 'BOLETA',
    estado VARCHAR(20) NOT NULL DEFAULT 'EMITIDO',
    fecha_emision DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    fecha_anulacion DATETIME2 NULL,
    motivo_anulacion VARCHAR(255) NULL,

    pago_id BIGINT NOT NULL UNIQUE,
    anulado_por_usuario_id BIGINT NULL,

    CONSTRAINT FK_Comprobante_Pago 
        FOREIGN KEY (pago_id) REFERENCES Pago(pago_id),

    CONSTRAINT FK_Comprobante_AnuladoPor 
        FOREIGN KEY (anulado_por_usuario_id) REFERENCES Usuario(usuario_id),

    CONSTRAINT CK_Comprobante_Tipo 
        CHECK (tipo_comprobante IN ('BOLETA')),

    CONSTRAINT CK_Comprobante_Estado 
        CHECK (estado IN ('EMITIDO', 'ANULADO'))
);