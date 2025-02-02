USE [master]
GO
/****** Object:  Database [Assortie]    Script Date: 24/03/2020 01:27:45 ******/
CREATE DATABASE [Assortie]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Assortie', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Assortie.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Assortie_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Assortie_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Assortie] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Assortie].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Assortie] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Assortie] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Assortie] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Assortie] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Assortie] SET ARITHABORT OFF 
GO
ALTER DATABASE [Assortie] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Assortie] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Assortie] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Assortie] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Assortie] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Assortie] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Assortie] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Assortie] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Assortie] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Assortie] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Assortie] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Assortie] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Assortie] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Assortie] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Assortie] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Assortie] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Assortie] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Assortie] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Assortie] SET  MULTI_USER 
GO
ALTER DATABASE [Assortie] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Assortie] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Assortie] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Assortie] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Assortie] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Assortie] SET QUERY_STORE = OFF
GO
USE [Assortie]
GO
/****** Object:  Table [dbo].[Adherent]    Script Date: 24/03/2020 01:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adherent](
	[IdAdherent] [int] IDENTITY(1,1) NOT NULL,
	[IdAssociation] [int] NOT NULL,
	[Matricule] [varchar](50) NOT NULL,
	[Nom] [varchar](100) NOT NULL,
	[Prenom] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Telephone] [varchar](50) NOT NULL,
	[Cotisation] [bit] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Responsable] [bit] NOT NULL,
	[Solde] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Adherent] PRIMARY KEY CLUSTERED 
(
	[IdAdherent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Association]    Script Date: 24/03/2020 01:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Association](
	[IdAssociation] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](100) NOT NULL,
	[MontantCotisation] [decimal](18, 2) NOT NULL,
	[Activite] [varchar](100) NOT NULL,
	[Telephone] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Association] PRIMARY KEY CLUSTERED 
(
	[IdAssociation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoriquePaiement]    Script Date: 24/03/2020 01:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoriquePaiement](
	[IdHistoriquePaiement] [int] IDENTITY(1,1) NOT NULL,
	[IdAdherent] [int] NOT NULL,
	[IdAssociation] [int] NOT NULL,
	[IdSortie] [int] NOT NULL,
	[Paiement] [decimal](18, 2) NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_HistoriquePaiement] PRIMARY KEY CLUSTERED 
(
	[IdHistoriquePaiement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sortie]    Script Date: 24/03/2020 01:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sortie](
	[IdSortie] [int] IDENTITY(1,1) NOT NULL,
	[IdAssociation] [int] NOT NULL,
	[Nom] [varchar](max) NOT NULL,
	[Prix] [decimal](18, 2) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Photo] [varchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[CapaciteActuelle] [int] NOT NULL,
	[CapaciteMaximum] [int] NOT NULL,
 CONSTRAINT [PK_Sortie] PRIMARY KEY CLUSTERED 
(
	[IdSortie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SortieAdherent]    Script Date: 24/03/2020 01:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SortieAdherent](
	[IdSortie] [int] NOT NULL,
	[IdAdherent] [int] NOT NULL,
	[IdAssociation] [int] NOT NULL,
 CONSTRAINT [PK_SortieAdherent] PRIMARY KEY CLUSTERED 
(
	[IdSortie] ASC,
	[IdAdherent] ASC,
	[IdAssociation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Adherent] ON 

INSERT [dbo].[Adherent] ([IdAdherent], [IdAssociation], [Matricule], [Nom], [Prenom], [Email], [Telephone], [Cotisation], [Login], [Password], [Responsable], [Solde]) VALUES (5, 2, N'100', N'ANASTASI', N'Bruno', N'bruno.anastasi@assortie.fr', N'0606060606', 1, N'anastasi', N'anastasi', 1, CAST(964.00 AS Decimal(18, 2)))
INSERT [dbo].[Adherent] ([IdAdherent], [IdAssociation], [Matricule], [Nom], [Prenom], [Email], [Telephone], [Cotisation], [Login], [Password], [Responsable], [Solde]) VALUES (7, 3, N'200', N'Rogogine', N'Maxime', N'maxime.rogogine@assortie.fr', N'0606060606', 0, N'rogogine', N'rogogine', 0, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Adherent] ([IdAdherent], [IdAssociation], [Matricule], [Nom], [Prenom], [Email], [Telephone], [Cotisation], [Login], [Password], [Responsable], [Solde]) VALUES (8, 2, N'102', N'Yeah', N'yah', N'bruno@bruno.fr', N'01010101', 0, N'bruno', N'bruno', 0, CAST(10000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Adherent] OFF
SET IDENTITY_INSERT [dbo].[Association] ON 

INSERT [dbo].[Association] ([IdAssociation], [Nom], [MontantCotisation], [Activite], [Telephone], [Email]) VALUES (2, N'Le Comptoir Sauvage', CAST(10.00 AS Decimal(18, 2)), N'Culture & Arts', N'0101010101', N'test@test.fr')
INSERT [dbo].[Association] ([IdAssociation], [Nom], [MontantCotisation], [Activite], [Telephone], [Email]) VALUES (3, N'La beauté du Geste', CAST(20.00 AS Decimal(18, 2)), N'Sport', N'0101010101', N'test@test.fr')
SET IDENTITY_INSERT [dbo].[Association] OFF
SET IDENTITY_INSERT [dbo].[Sortie] ON 

INSERT [dbo].[Sortie] ([IdSortie], [IdAssociation], [Nom], [Prix], [Description], [Photo], [Date], [CapaciteActuelle], [CapaciteMaximum]) VALUES (1, 2, N'Escape Game', CAST(20.00 AS Decimal(18, 2)), N'Echappez vous !', N'/Images/escape.jpg', CAST(N'2020-03-20' AS Date), 0, 20)
INSERT [dbo].[Sortie] ([IdSortie], [IdAssociation], [Nom], [Prix], [Description], [Photo], [Date], [CapaciteActuelle], [CapaciteMaximum]) VALUES (2, 3, N'Violon', CAST(20.00 AS Decimal(18, 2)), N'Pourquoi ne pas essayer ?', N'/Images/violon.jpg', CAST(N'2020-03-08' AS Date), 1, 10)
INSERT [dbo].[Sortie] ([IdSortie], [IdAssociation], [Nom], [Prix], [Description], [Photo], [Date], [CapaciteActuelle], [CapaciteMaximum]) VALUES (4, 2, N'Apiculture', CAST(10.00 AS Decimal(18, 2)), N'Initiation à l''apiculture', N'/Images/abeilles.jpg', CAST(N'2020-03-09' AS Date), 1, 10)
INSERT [dbo].[Sortie] ([IdSortie], [IdAssociation], [Nom], [Prix], [Description], [Photo], [Date], [CapaciteActuelle], [CapaciteMaximum]) VALUES (5, 2, N'Aidons les animaux', CAST(15.00 AS Decimal(18, 2)), N'Apprendre à les connaîtres', N'/Images/singes.jpg', CAST(N'2020-03-09' AS Date), 1, 20)
SET IDENTITY_INSERT [dbo].[Sortie] OFF
ALTER TABLE [dbo].[Adherent]  WITH CHECK ADD  CONSTRAINT [FK_Adherent_Association] FOREIGN KEY([IdAssociation])
REFERENCES [dbo].[Association] ([IdAssociation])
GO
ALTER TABLE [dbo].[Adherent] CHECK CONSTRAINT [FK_Adherent_Association]
GO
ALTER TABLE [dbo].[HistoriquePaiement]  WITH CHECK ADD  CONSTRAINT [FK_HistoriquePaiement_Adherent] FOREIGN KEY([IdAdherent])
REFERENCES [dbo].[Adherent] ([IdAdherent])
GO
ALTER TABLE [dbo].[HistoriquePaiement] CHECK CONSTRAINT [FK_HistoriquePaiement_Adherent]
GO
ALTER TABLE [dbo].[HistoriquePaiement]  WITH CHECK ADD  CONSTRAINT [FK_HistoriquePaiement_Association] FOREIGN KEY([IdAssociation])
REFERENCES [dbo].[Association] ([IdAssociation])
GO
ALTER TABLE [dbo].[HistoriquePaiement] CHECK CONSTRAINT [FK_HistoriquePaiement_Association]
GO
ALTER TABLE [dbo].[HistoriquePaiement]  WITH CHECK ADD  CONSTRAINT [FK_HistoriquePaiement_Sortie] FOREIGN KEY([IdSortie])
REFERENCES [dbo].[Sortie] ([IdSortie])
GO
ALTER TABLE [dbo].[HistoriquePaiement] CHECK CONSTRAINT [FK_HistoriquePaiement_Sortie]
GO
ALTER TABLE [dbo].[Sortie]  WITH CHECK ADD  CONSTRAINT [FK_Sortie_Association] FOREIGN KEY([IdAssociation])
REFERENCES [dbo].[Association] ([IdAssociation])
GO
ALTER TABLE [dbo].[Sortie] CHECK CONSTRAINT [FK_Sortie_Association]
GO
ALTER TABLE [dbo].[SortieAdherent]  WITH CHECK ADD  CONSTRAINT [FK_SortieAdherent_Adherent] FOREIGN KEY([IdAdherent])
REFERENCES [dbo].[Adherent] ([IdAdherent])
GO
ALTER TABLE [dbo].[SortieAdherent] CHECK CONSTRAINT [FK_SortieAdherent_Adherent]
GO
ALTER TABLE [dbo].[SortieAdherent]  WITH CHECK ADD  CONSTRAINT [FK_SortieAdherent_Association] FOREIGN KEY([IdAssociation])
REFERENCES [dbo].[Association] ([IdAssociation])
GO
ALTER TABLE [dbo].[SortieAdherent] CHECK CONSTRAINT [FK_SortieAdherent_Association]
GO
ALTER TABLE [dbo].[SortieAdherent]  WITH CHECK ADD  CONSTRAINT [FK_SortieAdherent_Sortie] FOREIGN KEY([IdSortie])
REFERENCES [dbo].[Sortie] ([IdSortie])
GO
ALTER TABLE [dbo].[SortieAdherent] CHECK CONSTRAINT [FK_SortieAdherent_Sortie]
GO
USE [master]
GO
ALTER DATABASE [Assortie] SET  READ_WRITE 
GO
