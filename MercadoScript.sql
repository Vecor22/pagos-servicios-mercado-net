USE [MercadoDB]
GO
/****** Object:  Table [dbo].[Comprobante]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprobante](
	[comprobante_id] [bigint] IDENTITY(1,1) NOT NULL,
	[numero_comprobante] [varchar](20) NOT NULL,
	[tipo_comprobante] [varchar](20) NOT NULL,
	[estado] [varchar](20) NOT NULL,
	[fecha_emision] [datetime2](7) NOT NULL,
	[fecha_anulacion] [datetime2](7) NULL,
	[motivo_anulacion] [varchar](255) NULL,
	[pago_id] [bigint] NOT NULL,
	[anulado_por_usuario_id] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[comprobante_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[numero_comprobante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[pago_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConceptoCobro]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptoCobro](
	[concepto_cobro_id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[tipo_cobro] [varchar](20) NOT NULL,
	[fecha_creacion] [datetime2](7) NOT NULL,
	[fecha_actualizacion] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[concepto_cobro_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deuda]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deuda](
	[deuda_id] [bigint] IDENTITY(1,1) NOT NULL,
	[codigo_deuda] [varchar](20) NOT NULL,
	[monto] [decimal](10, 2) NOT NULL,
	[estado] [varchar](20) NOT NULL,
	[tipo_generacion] [varchar](20) NOT NULL,
	[observacion] [varchar](255) NULL,
	[fecha_generacion] [datetime2](7) NOT NULL,
	[fecha_exoneracion] [datetime2](7) NULL,
	[motivo_exoneracion] [varchar](255) NULL,
	[concepto_cobro_id] [bigint] NOT NULL,
	[puesto_id] [bigint] NULL,
	[socio_id] [bigint] NULL,
	[deuda_origen_id] [bigint] NULL,
	[creado_por_usuario_id] [bigint] NOT NULL,
	[exonerado_por_usuario_id] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[deuda_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codigo_deuda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[pago_id] [bigint] IDENTITY(1,1) NOT NULL,
	[codigo_pago] [varchar](20) NOT NULL,
	[monto_pagado] [decimal](10, 2) NOT NULL,
	[medio_pago] [varchar](30) NOT NULL,
	[numero_operacion] [varchar](50) NULL,
	[estado] [varchar](20) NOT NULL,
	[fecha_pago] [datetime2](7) NOT NULL,
	[fecha_anulacion] [datetime2](7) NULL,
	[motivo_anulacion] [varchar](255) NULL,
	[deuda_id] [bigint] NOT NULL,
	[registrado_por_usuario_id] [bigint] NOT NULL,
	[anulado_por_usuario_id] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[pago_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codigo_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[deuda_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puesto]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puesto](
	[puesto_id] [bigint] IDENTITY(1,1) NOT NULL,
	[codigo_puesto] [varchar](20) NOT NULL,
	[fecha_creacion] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[puesto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codigo_puesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[rol_id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[activo] [bit] NOT NULL,
	[fecha_creacion] [datetime2](7) NOT NULL,
	[fecha_actualizacion] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[rol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socio]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socio](
	[socio_id] [bigint] IDENTITY(1,1) NOT NULL,
	[codigo_socio] [varchar](20) NOT NULL,
	[nombres] [varchar](100) NOT NULL,
	[apellidos] [varchar](100) NOT NULL,
	[dni] [char](8) NOT NULL,
	[correo] [varchar](120) NULL,
	[telefono] [varchar](20) NULL,
	[estado] [varchar](20) NOT NULL,
	[fecha_creacion] [datetime2](7) NOT NULL,
	[fecha_actualizacion] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[socio_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codigo_socio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocioPuesto]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocioPuesto](
	[socio_puesto_id] [bigint] IDENTITY(1,1) NOT NULL,
	[socio_id] [bigint] NOT NULL,
	[puesto_id] [bigint] NOT NULL,
	[asignado_por_usuario_id] [bigint] NOT NULL,
	[fecha_asignacion] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[socio_puesto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_SocioPuesto_Puesto] UNIQUE NONCLUSTERED 
(
	[puesto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[usuario_id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](255) NOT NULL,
	[nombre_completo] [varchar](150) NOT NULL,
	[foto_url] [varchar](255) NULL,
	[estado] [varchar](20) NOT NULL,
	[fecha_creacion] [datetime2](7) NOT NULL,
	[fecha_actualizacion] [datetime2](7) NULL,
	[rol_id] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comprobante] ADD  DEFAULT ('BOLETA') FOR [tipo_comprobante]
GO
ALTER TABLE [dbo].[Comprobante] ADD  DEFAULT ('EMITIDO') FOR [estado]
GO
ALTER TABLE [dbo].[Comprobante] ADD  DEFAULT (sysdatetime()) FOR [fecha_emision]
GO
ALTER TABLE [dbo].[ConceptoCobro] ADD  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Deuda] ADD  DEFAULT ('PENDIENTE') FOR [estado]
GO
ALTER TABLE [dbo].[Deuda] ADD  DEFAULT (sysdatetime()) FOR [fecha_generacion]
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT ('REGISTRADO') FOR [estado]
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT (sysdatetime()) FOR [fecha_pago]
GO
ALTER TABLE [dbo].[Puesto] ADD  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Socio] ADD  DEFAULT ('ACTIVO') FOR [estado]
GO
ALTER TABLE [dbo].[Socio] ADD  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[SocioPuesto] ADD  DEFAULT (sysdatetime()) FOR [fecha_asignacion]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ('ACTIVO') FOR [estado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Comprobante]  WITH CHECK ADD  CONSTRAINT [FK_Comprobante_AnuladoPor] FOREIGN KEY([anulado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Comprobante] CHECK CONSTRAINT [FK_Comprobante_AnuladoPor]
GO
ALTER TABLE [dbo].[Comprobante]  WITH CHECK ADD  CONSTRAINT [FK_Comprobante_Pago] FOREIGN KEY([pago_id])
REFERENCES [dbo].[Pago] ([pago_id])
GO
ALTER TABLE [dbo].[Comprobante] CHECK CONSTRAINT [FK_Comprobante_Pago]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_ConceptoCobro] FOREIGN KEY([concepto_cobro_id])
REFERENCES [dbo].[ConceptoCobro] ([concepto_cobro_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_ConceptoCobro]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_CreadoPor] FOREIGN KEY([creado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_CreadoPor]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_DeudaOrigen] FOREIGN KEY([deuda_origen_id])
REFERENCES [dbo].[Deuda] ([deuda_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_DeudaOrigen]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_ExoneradoPor] FOREIGN KEY([exonerado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_ExoneradoPor]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_Puesto] FOREIGN KEY([puesto_id])
REFERENCES [dbo].[Puesto] ([puesto_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_Puesto]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [FK_Deuda_Socio] FOREIGN KEY([socio_id])
REFERENCES [dbo].[Socio] ([socio_id])
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [FK_Deuda_Socio]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_AnuladoPor] FOREIGN KEY([anulado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_AnuladoPor]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Deuda] FOREIGN KEY([deuda_id])
REFERENCES [dbo].[Deuda] ([deuda_id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Deuda]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_RegistradoPor] FOREIGN KEY([registrado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_RegistradoPor]
GO
ALTER TABLE [dbo].[SocioPuesto]  WITH CHECK ADD  CONSTRAINT [FK_SocioPuesto_Puesto] FOREIGN KEY([puesto_id])
REFERENCES [dbo].[Puesto] ([puesto_id])
GO
ALTER TABLE [dbo].[SocioPuesto] CHECK CONSTRAINT [FK_SocioPuesto_Puesto]
GO
ALTER TABLE [dbo].[SocioPuesto]  WITH CHECK ADD  CONSTRAINT [FK_SocioPuesto_Socio] FOREIGN KEY([socio_id])
REFERENCES [dbo].[Socio] ([socio_id])
GO
ALTER TABLE [dbo].[SocioPuesto] CHECK CONSTRAINT [FK_SocioPuesto_Socio]
GO
ALTER TABLE [dbo].[SocioPuesto]  WITH CHECK ADD  CONSTRAINT [FK_SocioPuesto_Usuario] FOREIGN KEY([asignado_por_usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[SocioPuesto] CHECK CONSTRAINT [FK_SocioPuesto_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([rol_id])
REFERENCES [dbo].[Rol] ([rol_id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Comprobante]  WITH CHECK ADD  CONSTRAINT [CK_Comprobante_Estado] CHECK  (([estado]='ANULADO' OR [estado]='EMITIDO'))
GO
ALTER TABLE [dbo].[Comprobante] CHECK CONSTRAINT [CK_Comprobante_Estado]
GO
ALTER TABLE [dbo].[Comprobante]  WITH CHECK ADD  CONSTRAINT [CK_Comprobante_Tipo] CHECK  (([tipo_comprobante]='BOLETA'))
GO
ALTER TABLE [dbo].[Comprobante] CHECK CONSTRAINT [CK_Comprobante_Tipo]
GO
ALTER TABLE [dbo].[ConceptoCobro]  WITH CHECK ADD  CONSTRAINT [CK_ConceptoCobro_Tipo] CHECK  (([tipo_cobro]='CONSUMO' OR [tipo_cobro]='FIJO'))
GO
ALTER TABLE [dbo].[ConceptoCobro] CHECK CONSTRAINT [CK_ConceptoCobro_Tipo]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [CK_Deuda_Estado] CHECK  (([estado]='DISTRIBUIDA' OR [estado]='EXONERADA' OR [estado]='PAGADA' OR [estado]='PENDIENTE'))
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [CK_Deuda_Estado]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [CK_Deuda_Monto] CHECK  (([monto]>=(1)))
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [CK_Deuda_Monto]
GO
ALTER TABLE [dbo].[Deuda]  WITH CHECK ADD  CONSTRAINT [CK_Deuda_TipoGeneracion] CHECK  (([tipo_generacion]='REPARTIBLE' OR [tipo_generacion]='INDIVIDUAL'))
GO
ALTER TABLE [dbo].[Deuda] CHECK CONSTRAINT [CK_Deuda_TipoGeneracion]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [CK_Pago_Estado] CHECK  (([estado]='ANULADO' OR [estado]='REGISTRADO'))
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [CK_Pago_Estado]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [CK_Pago_Monto] CHECK  (([monto_pagado]>=(1)))
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [CK_Pago_Monto]
GO
ALTER TABLE [dbo].[Socio]  WITH CHECK ADD  CONSTRAINT [CK_Socio_Dni] CHECK  (([dni] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Socio] CHECK CONSTRAINT [CK_Socio_Dni]
GO
ALTER TABLE [dbo].[Socio]  WITH CHECK ADD  CONSTRAINT [CK_Socio_Estado] CHECK  (([estado]='INACTIVO' OR [estado]='ACTIVO'))
GO
ALTER TABLE [dbo].[Socio] CHECK CONSTRAINT [CK_Socio_Estado]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_Estado] CHECK  (([estado]='INACTIVO' OR [estado]='ACTIVO'))
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_Estado]
GO
/****** Object:  StoredProcedure [dbo].[usp_Auth_Login]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   AUTH - LOGIN
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Auth_Login]
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
/****** Object:  StoredProcedure [dbo].[usp_Comprobante_AnularPorPago]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Comprobante_AnularPorPago]
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
/****** Object:  StoredProcedure [dbo].[usp_Comprobante_BuscarPorIdPago]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------


CREATE   PROCEDURE [dbo].[usp_Comprobante_BuscarPorIdPago]
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
/****** Object:  StoredProcedure [dbo].[usp_Comprobante_BuscarPorNumero]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


----------------------------------------------------------------------


CREATE   PROCEDURE [dbo].[usp_Comprobante_BuscarPorNumero]
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
/****** Object:  StoredProcedure [dbo].[usp_Comprobante_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Comprobante_Crear]
    @pago_id BIGINT,
    @usuario_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @numero VARCHAR(20);

    SET @numero = 'BOL-' + RIGHT('000000' + CAST(NEXT VALUE FOR Seq_Comprobante AS VARCHAR(10)),6);

    INSERT INTO Comprobante (
        numero_comprobante,
        tipo_comprobante,
        estado,
        fecha_emision,
        pago_id,
        anulado_por_usuario_id -- o mejor: creado_por_usuario_id si tu tabla lo tuviera
    )
    VALUES (
        @numero,
        'BOLETA',
        'EMITIDO',
        SYSDATETIME(),
        @pago_id,
        NULL
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_ConceptoCobro_Actualizar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------
-------   (no se elimina, solo se modifica)   --------
------------------------------------------------------
CREATE   PROCEDURE [dbo].[usp_ConceptoCobro_Actualizar]
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
/****** Object:  StoredProcedure [dbo].[usp_ConceptoCobro_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_ConceptoCobro_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_ConceptoCobro_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_ConceptoCobro_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_ConceptoCobro_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   CONCEPTO COBRO
========================================================= */


CREATE   PROCEDURE [dbo].[usp_ConceptoCobro_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_BuscarPorCodigo]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [dbo].[usp_Deuda_BuscarPorCodigo]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [dbo].[usp_Deuda_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------------------------------------------------------------------

-----  SE GENERARÁ DEUDA PADRE CUANDO LA DEUDA SEA REPARTIBLE O SEA ASIGNADA A UN PUESTO SIN ASIGNACIÓN  -----
CREATE   PROCEDURE [dbo].[usp_Deuda_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_Exonerar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Deuda_Exonerar]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_GenerarHijasDistribuidas]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



---------------------   GENERAR DEUDAS HIJAS     ---------------------
----- SI NO HAY SOCIOS ACTIVOS FINALIZARÁ
----- 

CREATE   PROCEDURE [dbo].[usp_Deuda_GenerarHijasDistribuidas]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Deuda_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_ListarPorCodigoPuesto]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Deuda_ListarPorCodigoPuesto]
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
/****** Object:  StoredProcedure [dbo].[usp_Deuda_ListarPorEstado]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--------------------------   DEUDA - FILTROS    --------------------------------

CREATE   PROCEDURE [dbo].[usp_Deuda_ListarPorEstado]
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Deuda
    WHERE estado = @estado;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_Deuda_ListarPorFecha]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Deuda_ListarPorFecha]
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Deuda
    WHERE CAST(fecha_generacion AS DATE) BETWEEN @fecha_inicio AND @fecha_fin;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_Pago_Anular]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Pago_Anular]
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
/****** Object:  StoredProcedure [dbo].[usp_Pago_BuscarPorCodigo]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_BuscarPorCodigo]
    @codigo_pago VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Pago WHERE codigo_pago = @codigo_pago;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_Pago_BuscarPorCodigoDeuda]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_BuscarPorCodigoDeuda]
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
/****** Object:  StoredProcedure [dbo].[usp_Pago_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_BuscarPorId]
    @pago_id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Pago WHERE pago_id = @pago_id;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_Pago_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


----------------------------------------------------------------
---- SE PAGA SI EL ESTADO DE DEUDA ES PENDIENTE ----
---- SE PAGA SI LA DEUDA EXISTE ----
---- SE PAGA SI LA DEUDA NO TIENE ESTADO REGISTRADO ----

CREATE   PROCEDURE [dbo].[usp_Pago_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Pago_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_Pago_ListarPorCodigoPuesto]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_ListarPorCodigoPuesto]
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
/****** Object:  StoredProcedure [dbo].[usp_Pago_ListarPorFechas]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Pago_ListarPorFechas]
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
/****** Object:  StoredProcedure [dbo].[usp_Puesto_BuscarPorCodigo]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Puesto_BuscarPorCodigo]
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
/****** Object:  StoredProcedure [dbo].[usp_Puesto_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Puesto_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_Puesto_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Puesto_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Puesto_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   PUESTO - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Puesto_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_DeudasPorEstado]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_DeudasPorEstado]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_DeudasPorEstadoEntreFechas]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_DeudasPorEstadoEntreFechas]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_ResumenDeudas]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_ResumenDeudas]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_ResumenIngresosAnio]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_ResumenIngresosAnio]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_ResumenIngresosDia]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   REPORTE
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Reporte_ResumenIngresosDia]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_ResumenIngresosEntreFechas]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_ResumenIngresosEntreFechas]
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
/****** Object:  StoredProcedure [dbo].[usp_Reporte_ResumenIngresosMes]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_Reporte_ResumenIngresosMes]
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
/****** Object:  StoredProcedure [dbo].[usp_Rol_Actualizar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Rol_Actualizar]
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
/****** Object:  StoredProcedure [dbo].[usp_Rol_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Rol_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_Rol_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Rol_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Rol_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   ROL - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Rol_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_Actualizar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Socio_Actualizar]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_BuscarPorDni]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Socio_BuscarPorDni]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Socio_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_BuscarPorNombre]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Socio_BuscarPorNombre]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_CambiarEstado]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_Socio_CambiarEstado]
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

    ------------ Regla: si se inactiva, se desasignan sus puestos --------------
    IF @estado = 'INACTIVO'
    BEGIN
        DELETE FROM SocioPuesto
        WHERE socio_id = @socio_id;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_Socio_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Socio_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Socio_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   SOCIO - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Socio_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_BuscarPorCodigoPuesto]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------

CREATE   PROCEDURE [dbo].[usp_SocioPuesto_BuscarPorCodigoPuesto]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_CrearAsignacion]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   SOCIO_PUESTO - CREAR ASIGNACIÓN
   Regla: socio debe estar ACTIVO y el puesto libre
========================================================= */
CREATE   PROCEDURE [dbo].[usp_SocioPuesto_CrearAsignacion]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   SOCIO_PUESTO
========================================================= */

CREATE   PROCEDURE [dbo].[usp_SocioPuesto_Listar]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_ListarPorDniSocio]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------
CREATE   PROCEDURE [dbo].[usp_SocioPuesto_ListarPorDniSocio]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_ListarPorNombreSocio]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------
CREATE   PROCEDURE [dbo].[usp_SocioPuesto_ListarPorNombreSocio]
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
/****** Object:  StoredProcedure [dbo].[usp_SocioPuesto_Reasignar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   SOCIO_PUESTO - REASIGNAR
   Cambia el socio de un puesto existente
========================================================= */
CREATE   PROCEDURE [dbo].[usp_SocioPuesto_Reasignar]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_Actualizar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_Actualizar]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_BuscarPorId]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_BuscarPorId]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_BuscarPorUsername]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_BuscarPorUsername]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_CambiarEstado]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_CambiarEstado]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_CambiarPassword]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_CambiarPassword]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_Crear]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Usuario_Crear]
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
/****** Object:  StoredProcedure [dbo].[usp_Usuario_Listar]    Script Date: 29/04/2026 1:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USUARIO - PROCEDIMIENTOS ALMACENADOS
========================================================= */

CREATE   PROCEDURE [dbo].[usp_Usuario_Listar]
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
