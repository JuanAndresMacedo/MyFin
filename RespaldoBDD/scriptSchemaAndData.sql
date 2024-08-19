USE [Blazor2]
GO
/****** Object:  Table [dbo].[CategoriaObjetivoDeGasto]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Categorias]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Cuenta]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Espacios]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[EspacioUsuario]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Monedas]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[ObjetivoDeGastos]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[TiposDeCambios]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Transacciones]    Script Date: 15/11/2023 16:16:22 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 15/11/2023 16:16:22 ******/
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
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (1, 1)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (5, 2)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (7, 3)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (5, 4)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (7, 4)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (11, 4)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (12, 4)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (12, 5)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (13, 6)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (14, 7)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (14, 8)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (14, 9)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (14, 10)
INSERT [dbo].[CategoriaObjetivoDeGasto] ([CategoriasId], [ObjetivosDeGastoId]) VALUES (14, 11)
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (1, 3, 4, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Comida', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (2, 3, 4, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Ropa', N'Costo', N'Inactiva')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (3, 3, 4, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Sueldo', N'Ingreso', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (4, 3, 4, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Regalos', N'Ingreso', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (5, 3, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Salidas', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (6, 1, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Ingresos', N'Ingreso', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (7, 1, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Comidas', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (8, 1, 1, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Casa', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (9, 1, 1, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Regalos', N'Ingreso', N'Inactiva')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (10, 1, 1, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Mudanza', N'Costo', N'Inactiva')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (11, 2, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Alquiler', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (12, 2, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Transporte', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (13, 3, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Pagos en dolares', N'Costo', N'Activa')
INSERT [dbo].[Categorias] ([Id], [UsuarioCreadorId], [EspacioId], [FechaDeCreacion], [Nombre], [Tipo], [Estatus]) VALUES (14, 3, 5, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Servicios', N'Costo', N'Activa')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (1, 3, 3, CAST(N'2020-10-15T00:00:00.0000000' AS DateTime2), N'Monetaria de Juan', 1, N'Monetaria', 6994, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (2, 3, 3, CAST(N'2019-12-21T00:00:00.0000000' AS DateTime2), N'Tarjeta de Juan', 2, N'TarjetaDeCredito', NULL, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), N'ITAU', N'1111', 4947.131)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (3, 3, 4, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Ahorros de Juan', 1, N'Monetaria', 42158, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (4, 1, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Tarjeta volar (Nico)', 3, N'TarjetaDeCredito', NULL, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), N'ITAU', N'4545', 1547.85876)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (5, 1, 1, CAST(N'2023-11-03T00:00:00.0000000' AS DateTime2), N'Ahorros', 2, N'Monetaria', 2700, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (6, 2, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Tarjeta Ignacio', 1, N'TarjetaDeCredito', NULL, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), N'BBVA', N'4323', 700000)
INSERT [dbo].[Cuenta] ([Id], [PropietarioId], [EspacioId], [FechaDeCreacion], [Nombre], [MonedaId], [Discriminator], [Monto], [FechaDeCierre], [BancoEmisor], [UltimosCuatroDigitos], [CreditoDisponible]) VALUES (7, 3, 5, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Cuenta de 1500 pesos', 1, N'Monetaria', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Espacios] ON 

INSERT [dbo].[Espacios] ([Id], [Nombre], [AdministradorId]) VALUES (1, N'Principal Nicolas', 1)
INSERT [dbo].[Espacios] ([Id], [Nombre], [AdministradorId]) VALUES (2, N'Principal Ignacio', 2)
INSERT [dbo].[Espacios] ([Id], [Nombre], [AdministradorId]) VALUES (3, N'Espacio facultad', 3)
INSERT [dbo].[Espacios] ([Id], [Nombre], [AdministradorId]) VALUES (4, N'Personal ', 3)
INSERT [dbo].[Espacios] ([Id], [Nombre], [AdministradorId]) VALUES (5, N'Espacio de prueba', 3)
SET IDENTITY_INSERT [dbo].[Espacios] OFF
GO
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [ParticipantesId]) VALUES (3, 1)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [ParticipantesId]) VALUES (3, 2)
GO
SET IDENTITY_INSERT [dbo].[Monedas] ON 

INSERT [dbo].[Monedas] ([Id], [Nombre], [SimboloMonetario]) VALUES (1, N'Peso Uruguayo', N'UYU')
INSERT [dbo].[Monedas] ([Id], [Nombre], [SimboloMonetario]) VALUES (2, N'Dólar', N'USD')
INSERT [dbo].[Monedas] ([Id], [Nombre], [SimboloMonetario]) VALUES (3, N'Euro', N'EUR')
SET IDENTITY_INSERT [dbo].[Monedas] OFF
GO
SET IDENTITY_INSERT [dbo].[ObjetivoDeGastos] ON 

INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (1, 3, 4, N'Comida de noviembre', 2000, N'8972c5a0-a182-452f-8f08-6b2bf6e94060')
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (2, 3, 3, N'Gastar menos de 4500 en salidas', 4500, N'1e43240f-a6bb-4eae-9ca6-c57e091d4c8e')
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (3, 1, 3, N'Gastar menos de 6000 en comida', 6000, NULL)
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (4, 2, 3, N'Gastar menos de 50000 al mes', 50000, N'8a081fdc-3663-4565-8a01-b29d66ea088a')
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (5, 2, 3, N'Gastar menos de 5000 en transporte', 5000, NULL)
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (6, 3, 3, N'Gastar pocos dolares', 250, NULL)
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (7, 3, 5, N'Gastar menos de 200', 200, NULL)
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (8, 3, 5, N'Gastar menos de 400', 400, N'9f0a1f6a-c0bb-48b3-8118-54f61c147393')
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (9, 3, 5, N'Gastar menos de 800', 800, NULL)
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (10, 3, 5, N'Gastar menos de 1499', 1499, N'a86fd6ed-8a0e-4ac1-89e7-4b5655a845ba')
INSERT [dbo].[ObjetivoDeGastos] ([Id], [UsuarioCreadorId], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (11, 3, 5, N'Gastar menos de 1500', 1500, NULL)
SET IDENTITY_INSERT [dbo].[ObjetivoDeGastos] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposDeCambios] ON 

INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (1, 1, 3, 3, CAST(N'2023-10-01T00:00:00.0000000' AS DateTime2), 35)
INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (2, 1, 3, 3, CAST(N'2023-10-02T00:00:00.0000000' AS DateTime2), 37.56)
INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (3, 1, 3, 3, CAST(N'2023-10-03T00:00:00.0000000' AS DateTime2), 36.8)
INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (4, 2, 3, 2, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 41)
INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (5, 2, 3, 2, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 39.5)
INSERT [dbo].[TiposDeCambios] ([Id], [UsuarioCreadorId], [EspacioId], [MonedaId], [Fecha], [ValorDeLaMoneda]) VALUES (6, 2, 3, 2, CAST(N'2023-11-17T00:00:00.0000000' AS DateTime2), 37.6)
SET IDENTITY_INSERT [dbo].[TiposDeCambios] OFF
GO
SET IDENTITY_INSERT [dbo].[Transacciones] ON 

INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (1, 4, CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), N'Costo', N'T1 Comida', 110, 1, 3, 1)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (2, 4, CAST(N'2023-11-02T00:00:00.0000000' AS DateTime2), N'Costo', N'T2 Comida', 230, 1, 3, 1)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (3, 4, CAST(N'2023-11-03T00:00:00.0000000' AS DateTime2), N'Costo', N'T3 Comida', 80, 1, 3, 1)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (4, 4, CAST(N'2023-11-04T00:00:00.0000000' AS DateTime2), N'Costo', N'T4', 550, 1, 3, 1)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (5, 4, CAST(N'2023-11-02T00:00:00.0000000' AS DateTime2), N'Ingreso', N'Regalo ', 2000, 1, 3, 4)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (6, 4, CAST(N'2023-11-03T00:00:00.0000000' AS DateTime2), N'Ingreso', N'Sueldo 4hs', 450, 1, 3, 3)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (7, 4, CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), N'Ingreso', N'Sueldo 4.3hs', 678, 1, 3, 3)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (8, 3, CAST(N'2023-11-23T00:00:00.0000000' AS DateTime2), N'Costo', N'Salida 1', 250, 1, 1, 5)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (9, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Costo', N'Salida 2', 456, 1, 1, 5)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (10, 3, CAST(N'2023-10-02T00:00:00.0000000' AS DateTime2), N'Costo', N'T1EUR', 10, 1, 4, 5)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (11, 3, CAST(N'2023-10-03T00:00:00.0000000' AS DateTime2), N'Costo', N'T2EUR', 10, 3, 4, 5)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (12, 3, CAST(N'2023-10-03T00:00:00.0000000' AS DateTime2), N'Ingreso', N'SUELDONICO', 27899, 1, 4, 6)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (13, 3, CAST(N'2023-10-03T00:00:00.0000000' AS DateTime2), N'Costo', N'ALQUILERIGNACIO', 15000, 1, 6, 11)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (14, 3, CAST(N'2023-10-02T00:00:00.0000000' AS DateTime2), N'Costo', N'ALQUILERJUAN', 25000, 1, 1, 11)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (15, 3, CAST(N'2023-10-01T00:00:00.0000000' AS DateTime2), N'Ingreso', N'SUELDOIGNACIO', 15000, 1, 6, 6)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (16, 3, CAST(N'2023-10-12T00:00:00.0000000' AS DateTime2), N'Ingreso', N'PAGOEXTRAJUAN', 2700, 1, 1, 6)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (17, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Costo', N'T1USD', 15, 2, 2, 13)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (18, 3, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), N'Costo', N'T2USD', 13.99, 2, 2, 13)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (19, 3, CAST(N'2023-11-17T00:00:00.0000000' AS DateTime2), N'Costo', N'T3USD', 19, 2, 2, 13)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (20, 3, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Costo', N'PESOS A USD', 200, 1, 2, 13)
INSERT [dbo].[Transacciones] ([Id], [EspacioId], [Fecha], [Tipo], [Nombre], [Monto], [MonedaId], [CuentaId], [CategoriaId]) VALUES (21, 5, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'Costo', N'PAGO DE PRUEBA', 1499, 1, 7, 14)
SET IDENTITY_INSERT [dbo].[Transacciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Correo], [Contrasena], [Nombre], [Apellido], [Direccion]) VALUES (1, N'Nico@email.com', N'N123456789', N'Nicolas', N'de la Hoz', N'Casa Nico')
INSERT [dbo].[Usuarios] ([Id], [Correo], [Contrasena], [Nombre], [Apellido], [Direccion]) VALUES (2, N'Ignacio@email.com', N'I123456789', N'Ignacio', N'Sena', N'')
INSERT [dbo].[Usuarios] ([Id], [Correo], [Contrasena], [Nombre], [Apellido], [Direccion]) VALUES (3, N'Juan@email.com', N'J123456789', N'Juan Andres', N'Macedo', N'Casa Juan 1234')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
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
