USE [master]
GO
CREATE DATABASE [BalearesChallenge]
GO
ALTER DATABASE [BalearesChallenge] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BalearesChallenge].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BalearesChallenge] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BalearesChallenge] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BalearesChallenge] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BalearesChallenge] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BalearesChallenge] SET ARITHABORT OFF 
GO
ALTER DATABASE [BalearesChallenge] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BalearesChallenge] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BalearesChallenge] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BalearesChallenge] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BalearesChallenge] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BalearesChallenge] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BalearesChallenge] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BalearesChallenge] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BalearesChallenge] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BalearesChallenge] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BalearesChallenge] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BalearesChallenge] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BalearesChallenge] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BalearesChallenge] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BalearesChallenge] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BalearesChallenge] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BalearesChallenge] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BalearesChallenge] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BalearesChallenge] SET  MULTI_USER 
GO
ALTER DATABASE [BalearesChallenge] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BalearesChallenge] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BalearesChallenge] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BalearesChallenge] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BalearesChallenge] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BalearesChallenge] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BalearesChallenge] SET QUERY_STORE = ON
GO
ALTER DATABASE [BalearesChallenge] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BalearesChallenge]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/5/2024 10:19:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacto]    Script Date: 12/5/2024 10:19:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacto](
	[IdContacto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Empresa] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[Telefono] [int] NOT NULL,
	[Direccion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Contacto] PRIMARY KEY CLUSTERED 
(
	[IdContacto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/5/2024 10:19:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Correo] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
	[Apellido] [nvarchar](25) NOT NULL,
	[Salt] [nvarchar](100) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241205024704_PrimeraMigracion', N'9.0.0')
GO
SET IDENTITY_INSERT [dbo].[Contacto] ON 
GO
INSERT [dbo].[Contacto] ([IdContacto], [Nombre], [Empresa], [Email], [FechaNacimiento], [Telefono], [Direccion]) VALUES (1, N'Laura García', N'Empresa A', N'laura@gmail.com', CAST(N'1985-06-15T00:00:00.000' AS DateTime), 123456789, N'Avenida de Mayo 123, Buenos Aires, CABA')
GO
INSERT [dbo].[Contacto] ([IdContacto], [Nombre], [Empresa], [Email], [FechaNacimiento], [Telefono], [Direccion]) VALUES (2, N'Miguel Sánchez', N'Empresa B', N'miguel@gmail.com', CAST(N'1990-08-22T00:00:00.000' AS DateTime), 987654321, N'Calle Corrientes 456, Córdoba, Córdoba')
GO
INSERT [dbo].[Contacto] ([IdContacto], [Nombre], [Empresa], [Email], [FechaNacimiento], [Telefono], [Direccion]) VALUES (3, N'Isabel Torres', N'Empresa C', N'isabel@gmail.com', CAST(N'1995-11-03T00:00:00.000' AS DateTime), 567890123, N'Calle San Martín 789, Rosario, Santa Fe')
GO
INSERT [dbo].[Contacto] ([IdContacto], [Nombre], [Empresa], [Email], [FechaNacimiento], [Telefono], [Direccion]) VALUES (4, N'José Martínez', N'Empresa D', N'jose@gmail.com', CAST(N'1982-02-25T00:00:00.000' AS DateTime), 345678901, N'Avenida 9 de Julio 101, Mendoza, Mendoza')
GO
INSERT [dbo].[Contacto] ([IdContacto], [Nombre], [Empresa], [Email], [FechaNacimiento], [Telefono], [Direccion]) VALUES (5, N'Sofía Ramírez', N'Empresa E', N'sofia@gmail.com', CAST(N'1998-04-17T00:00:00.000' AS DateTime), 234567890, N'Calle Libertador 202, La Plata, Buenos Aires')
GO
SET IDENTITY_INSERT [dbo].[Contacto] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Correo], [Password], [Nombre], [Apellido], [Salt]) VALUES (1, N'user1@gmail.com', N'68e800f6c106fb44892297a2f43a2bee835e7eed5c7663bb39b7360ad053984d', N'Juan', N'Pérez', N'X4fWSp2PAPEbLoeZkfPAEQ==')
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Correo], [Password], [Nombre], [Apellido], [Salt]) VALUES (2, N'user2@gmail.com', N'4b032834cbda2fc8e430154be776b33533f4465d04ea556132f0ff49cb46a71a', N'Ana', N'López', N'Pn1j5jCfYkjtmGGcsM4cUg==')
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Correo], [Password], [Nombre], [Apellido], [Salt]) VALUES (3, N'user3@gmail.com', N'2114cbb1501d50f010a81f84368f409760e2a94167e72f71cf7847e17ccdca03', N'Carlos', N'Martínez', N'iygXzStg396QmCs4cBfl3A==')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
USE [master]
GO
ALTER DATABASE [BalearesChallenge] SET  READ_WRITE 
GO
