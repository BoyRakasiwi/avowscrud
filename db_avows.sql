USE [master]
GO
/****** Object:  Database [avows]    Script Date: 6/20/2022 10:24:56 AM ******/
CREATE DATABASE [avows]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'avows', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\avows.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'avows_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\avows_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [avows] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [avows].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [avows] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [avows] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [avows] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [avows] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [avows] SET ARITHABORT OFF 
GO
ALTER DATABASE [avows] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [avows] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [avows] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [avows] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [avows] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [avows] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [avows] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [avows] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [avows] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [avows] SET  DISABLE_BROKER 
GO
ALTER DATABASE [avows] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [avows] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [avows] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [avows] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [avows] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [avows] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [avows] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [avows] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [avows] SET  MULTI_USER 
GO
ALTER DATABASE [avows] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [avows] SET DB_CHAINING OFF 
GO
ALTER DATABASE [avows] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [avows] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [avows] SET DELAYED_DURABILITY = DISABLED 
GO
USE [avows]
GO
/****** Object:  Table [dbo].[Produk]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produk](
	[Kode_Barang] [int] IDENTITY(1,1) NOT NULL,
	[Nama_Barang] [varchar](50) NOT NULL,
	[Harga] [int] NOT NULL,
	[Keterangan] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Produk] ON 

INSERT [dbo].[Produk] ([Kode_Barang], [Nama_Barang], [Harga], [Keterangan]) VALUES (1, N'Tas', 2, N'pcs       ')
INSERT [dbo].[Produk] ([Kode_Barang], [Nama_Barang], [Harga], [Keterangan]) VALUES (5, N'Sepatu', 750000, N'Skechers')
SET IDENTITY_INSERT [dbo].[Produk] OFF
/****** Object:  StoredProcedure [dbo].[hapus]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[hapus]
@kode_barang int,@OUTPUTMESSAGE varchar(50) output
as
begin
declare @rowcount int=0
begin try
	set @rowcount=(select COUNT(1) from Produk where Kode_Barang=@kode_barang)

	if(@rowcount>0)
	begin
	begin tran
		delete from Produk where Kode_Barang=@kode_barang

		set @OUTPUTMESSAGE='Produk berhasil dihapus'
	commit  tran
	end
	else
		begin
			set @OUTPUTMESSAGE='Produk tidak ada dengan kode ' + convert(varchar,@kode_barang)
		end
end try
begin catch
rollback tran
set @OUTPUTMESSAGE=ERROR_MESSAGE()
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[Show_data]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Show_data]
as
begin
select Kode_Barang,Nama_Barang,Harga,Keterangan 
from Produk
end
GO
/****** Object:  StoredProcedure [dbo].[simpan]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[simpan]
@nama_barang varchar(50),
@harga int,@keterangan  varchar(50)=null
as
begin
declare @RowCount int=0

set @RowCount=(select count(1) from Produk where Nama_Barang=@nama_barang)

begin try
	begin tran
	if (@RowCount=0)
		begin
			insert into Produk(Nama_Barang,Harga,Keterangan) values (@nama_barang,@harga,@keterangan)
		end
	commit  tran
end try
begin catch
rollback tran
select ERROR_MESSAGE()
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[tampil]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tampil]
@kode_barang int
as
begin
select Kode_Barang,Nama_Barang,Harga,Keterangan
from Produk where Kode_Barang=@kode_barang
end
GO
/****** Object:  StoredProcedure [dbo].[ubah]    Script Date: 6/20/2022 10:24:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ubah]
@Kode_Barang int,@nama_barang varchar(50),
@harga int,@keterangan  varchar(50)=null
as
begin
declare @RowCount int=0

set @RowCount=(select count(1) from Produk where Nama_Barang=@nama_barang and Kode_Barang<>@Kode_Barang)

begin try
	begin tran
	if (@RowCount=0)
		begin
			update Produk set Nama_Barang=@nama_barang,Harga=@harga,Keterangan=@keterangan
			where Kode_Barang=@Kode_Barang
		end
	commit  tran
end try
begin catch
rollback tran
select ERROR_MESSAGE()
end catch
end
GO
USE [master]
GO
ALTER DATABASE [avows] SET  READ_WRITE 
GO
