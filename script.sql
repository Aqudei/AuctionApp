USE [master]
GO
/****** Object:  Database [AuctionContext]    Script Date: 2/14/2019 6:40:45 PM ******/
CREATE DATABASE [AuctionContext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AuctionContext', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AuctionContext.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AuctionContext_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AuctionContext_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AuctionContext] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AuctionContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AuctionContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AuctionContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AuctionContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AuctionContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AuctionContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [AuctionContext] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AuctionContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AuctionContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AuctionContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AuctionContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AuctionContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AuctionContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AuctionContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AuctionContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AuctionContext] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AuctionContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AuctionContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AuctionContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AuctionContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AuctionContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AuctionContext] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AuctionContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AuctionContext] SET RECOVERY FULL 
GO
ALTER DATABASE [AuctionContext] SET  MULTI_USER 
GO
ALTER DATABASE [AuctionContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AuctionContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AuctionContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AuctionContext] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AuctionContext] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AuctionContext', N'ON'
GO
USE [AuctionContext]
GO
/****** Object:  User [auction]    Script Date: 2/14/2019 6:40:45 PM ******/
CREATE USER [auction] FOR LOGIN [auction] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [auction]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [auction]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [auction]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [auction]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [auction]
GO
ALTER ROLE [db_datareader] ADD MEMBER [auction]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [auction]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [auction]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [auction]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2/14/2019 6:40:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 2/14/2019 6:40:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountType] [int] NOT NULL,
	[UserName] [nvarchar](32) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bids]    Script Date: 2/14/2019 6:40:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bids](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[BidAmount] [float] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[BidDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Bids] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/14/2019 6:40:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[InitialPrice] [float] NOT NULL,
	[LastBidAmount] [float] NOT NULL,
	[Close] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [AuctionContext] SET  READ_WRITE 
GO
