USE [Blazor2]
GO
/****** Object:  Table [dbo].[CategoriaObjetivoDeGasto]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriaObjetivoDeGasto](
	[CategoriasId] [int] NOT NULL,
	[ObjetivosDeGastoId] [int] NOT NULL,
 CONSTRAINT [PK_CategoriaObjetivoDeGasto] PRIMARY KEY CLUSTERED 
(
	[CategoriasId] ASC,
	[ObjetivosDeGastoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioCreadorId] [int] NOT NULL,
	[EspacioId] [int] NOT NULL,
	[FechaDeCreacion] [datetime2](7) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Tipo] [nvarchar](max) NULL,
	[Estatus] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropietarioId] [int] NOT NULL,
	[EspacioId] [int] NOT NULL,
	[FechaDeCreacion] [datetime2](7) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[MonedaId] [int] NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Monto] [real] NULL,
	[FechaDeCierre] [datetime2](7) NULL,
	[BancoEmisor] [nvarchar](max) NULL,
	[UltimosCuatroDigitos] [nvarchar](max) NULL,
	[CreditoDisponible] [real] NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Espacios]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Espacios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[AdministradorId] [int] NOT NULL,
 CONSTRAINT [PK_Espacios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EspacioUsuario]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EspacioUsuario](
	[EspaciosId] [int] NOT NULL,
	[ParticipantesId] [int] NOT NULL,
 CONSTRAINT [PK_EspacioUsuario] PRIMARY KEY CLUSTERED 
(
	[EspaciosId] ASC,
	[ParticipantesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monedas]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monedas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[SimboloMonetario] [nvarchar](max) NULL,
 CONSTRAINT [PK_Monedas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjetivoDeGastos]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjetivoDeGastos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioCreadorId] [int] NOT NULL,
	[EspacioId] [int] NOT NULL,
	[Titulo] [nvarchar](max) NULL,
	[MontoMaximo] [real] NOT NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_ObjetivoDeGastos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposDeCambios]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposDeCambios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioCreadorId] [int] NOT NULL,
	[EspacioId] [int] NOT NULL,
	[MonedaId] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[ValorDeLaMoneda] [real] NULL,
 CONSTRAINT [PK_TiposDeCambios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspacioId] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Tipo] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Monto] [real] NULL,
	[MonedaId] [int] NULL,
	[CuentaId] [int] NULL,
	[CategoriaId] [int] NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 15/11/2023 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Correo] [nvarchar](max) NULL,
	[Contrasena] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[Direccion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoriaObjetivoDeGasto]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaObjetivoDeGasto_Categorias_CategoriasId] FOREIGN KEY([CategoriasId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[CategoriaObjetivoDeGasto] CHECK CONSTRAINT [FK_CategoriaObjetivoDeGasto_Categorias_CategoriasId]
GO
ALTER TABLE [dbo].[CategoriaObjetivoDeGasto]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaObjetivoDeGasto_ObjetivoDeGastos_ObjetivosDeGastoId] FOREIGN KEY([ObjetivosDeGastoId])
REFERENCES [dbo].[ObjetivoDeGastos] ([Id])
GO
ALTER TABLE [dbo].[CategoriaObjetivoDeGasto] CHECK CONSTRAINT [FK_CategoriaObjetivoDeGasto_ObjetivoDeGastos_ObjetivosDeGastoId]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Usuarios_UsuarioCreadorId] FOREIGN KEY([UsuarioCreadorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Usuarios_UsuarioCreadorId]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Monedas_MonedaId] FOREIGN KEY([MonedaId])
REFERENCES [dbo].[Monedas] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Monedas_MonedaId]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Usuarios_PropietarioId] FOREIGN KEY([PropietarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Usuarios_PropietarioId]
GO
ALTER TABLE [dbo].[Espacios]  WITH CHECK ADD  CONSTRAINT [FK_Espacios_Usuarios_AdministradorId] FOREIGN KEY([AdministradorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Espacios] CHECK CONSTRAINT [FK_Espacios_Usuarios_AdministradorId]
GO
ALTER TABLE [dbo].[EspacioUsuario]  WITH CHECK ADD  CONSTRAINT [FK_EspacioUsuario_Espacios_EspaciosId] FOREIGN KEY([EspaciosId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[EspacioUsuario] CHECK CONSTRAINT [FK_EspacioUsuario_Espacios_EspaciosId]
GO
ALTER TABLE [dbo].[EspacioUsuario]  WITH CHECK ADD  CONSTRAINT [FK_EspacioUsuario_Usuarios_ParticipantesId] FOREIGN KEY([ParticipantesId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[EspacioUsuario] CHECK CONSTRAINT [FK_EspacioUsuario_Usuarios_ParticipantesId]
GO
ALTER TABLE [dbo].[ObjetivoDeGastos]  WITH CHECK ADD  CONSTRAINT [FK_ObjetivoDeGastos_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[ObjetivoDeGastos] CHECK CONSTRAINT [FK_ObjetivoDeGastos_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[ObjetivoDeGastos]  WITH CHECK ADD  CONSTRAINT [FK_ObjetivoDeGastos_Usuarios_UsuarioCreadorId] FOREIGN KEY([UsuarioCreadorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[ObjetivoDeGastos] CHECK CONSTRAINT [FK_ObjetivoDeGastos_Usuarios_UsuarioCreadorId]
GO
ALTER TABLE [dbo].[TiposDeCambios]  WITH CHECK ADD  CONSTRAINT [FK_TiposDeCambios_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[TiposDeCambios] CHECK CONSTRAINT [FK_TiposDeCambios_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[TiposDeCambios]  WITH CHECK ADD  CONSTRAINT [FK_TiposDeCambios_Monedas_MonedaId] FOREIGN KEY([MonedaId])
REFERENCES [dbo].[Monedas] ([Id])
GO
ALTER TABLE [dbo].[TiposDeCambios] CHECK CONSTRAINT [FK_TiposDeCambios_Monedas_MonedaId]
GO
ALTER TABLE [dbo].[TiposDeCambios]  WITH CHECK ADD  CONSTRAINT [FK_TiposDeCambios_Usuarios_UsuarioCreadorId] FOREIGN KEY([UsuarioCreadorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[TiposDeCambios] CHECK CONSTRAINT [FK_TiposDeCambios_Usuarios_UsuarioCreadorId]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Cuenta_CuentaId] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Cuenta_CuentaId]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Monedas_MonedaId] FOREIGN KEY([MonedaId])
REFERENCES [dbo].[Monedas] ([Id])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Monedas_MonedaId]
GO
