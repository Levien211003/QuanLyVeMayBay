SET QUOTED_IDENTIFIER OFF;
DROP DATABASE IF EXISTS BookingAirLine;
GO

CREATE DATABASE BookingAirLine;
USE BookingAirLine;
GO



CREATE TABLE [dbo].[HangHangKhongs] (
    [MaHangHang] INT IDENTITY(1, 1) PRIMARY KEY,
    [HinhAnh] VARCHAR(255),
    [TenHH] VARCHAR(255)
);
GO

CREATE TABLE [dbo].[TuyenBays] (
    [MaTBay] INT IDENTITY(1, 1) NOT NULL,
    [SanBayDi] NVARCHAR(50) NULL,
    [SanBayDen] NVARCHAR(50) NULL,
    CONSTRAINT [PK_TuyenBays] PRIMARY KEY ([MaTBay])
);
GO

CREATE TABLE [dbo].[ChuyenBays] (
    [MaCB] INT IDENTITY(1, 1) NOT NULL,
    [MaTBay] INT NULL,
    [MaHangHang] INT NULL,
    [NgayGio] DATETIME NULL,
	[Gia] DECIMAL(18, 2) NULL,
    [ThoiGianBay] INT NULL,
    [SoLuongGheHang1] INT NULL,
    [SoLuongGheHang2] INT NULL,
    [SoLuongGheHang3] INT NULL,
    [TinhTrang] NVARCHAR(50) NULL,
    [HinhAnh] NVARCHAR(100) NULL,
    CONSTRAINT [PK_ChuyenBays] PRIMARY KEY ([MaCB]),
    CONSTRAINT [FK_ChuyenBays_TuyenBays] FOREIGN KEY ([MaTBay]) REFERENCES [dbo].[TuyenBays] ([MaTBay]),
    CONSTRAINT [FK_ChuyenBays_HangHangKhong] FOREIGN KEY ([MaHangHang]) REFERENCES [dbo].[HangHangKhongs] ([MaHangHang])
);
GO
CREATE TABLE [dbo].[Customers] (
    [customer_id] INT IDENTITY(1, 1) NOT NULL,
    [full_name] NVARCHAR(50) NULL,
    [NgaySinh] Date NULL,
    [phone_number] NVARCHAR(20) NULL,
    [email] NVARCHAR(50) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([customer_id])
);
GO
CREATE TABLE [dbo].[Accounts] (
    [customer_id] INT IDENTITY(1, 1) NOT NULL,
    [username] NVARCHAR(50) NULL,
    [password] NVARCHAR(50) NULL,
    [is_admin] BIT NULL,
	 [HoTen] NVARCHAR(50) NULL,
    [NgaySinh] Date NULL,
    [SoDienThoai] NVARCHAR(20) NULL,
    [Email] NVARCHAR(50) NULL,

    CONSTRAINT [PK_Accounts] PRIMARY KEY ([customer_id]),
    CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customers] ([customer_id])
);
GO


CREATE TABLE [dbo].[Bookings] (
    [booking_id] INT IDENTITY(1, 1) NOT NULL,
    [MaCB] INT NULL,
    [customer_id] INT NULL,
    [ngay_bay] DATETIME NULL,
    [trang_thai] NVARCHAR(50) NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([booking_id]),
    CONSTRAINT [FK_Bookings_ChuyenBays] FOREIGN KEY ([MaCB]) REFERENCES [dbo].[ChuyenBays] ([MaCB]),
    CONSTRAINT [FK_Bookings_Customers] FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customers] ([customer_id])
);
GO
use BookingAirLine
Create TABLE Guest (
    Id_Guest INT IDENTITY(1, 1) PRIMARY KEY,
    HoTenG NVARCHAR(MAX),
    EmailG NVARCHAR(MAX),
    SoDienThoaiG NVARCHAR(MAX),
    NgaySinhG DATE,
    SoHoChieu NVARCHAR(MAX),
    QuocGiaCap NVARCHAR(MAX),
    NgayHetHan DATE
);
ALTER TABLE Bookings
ADD Id_Guest INT NULL,
    CONSTRAINT FK_Bookings_Guest FOREIGN KEY (Id_Guest) REFERENCES Guest (Id_Guest);

CREATE TABLE [dbo].[Payments] (
    [payment_id] INT IDENTITY(1, 1) NOT NULL,
    [booking_id] INT NULL,
    [payment_date] DATETIME NULL,
    [payment_method] NVARCHAR(50) NULL,
    [amount] DECIMAL(10, 2) NULL,
    [customer_id] INT NULL,
    [payment_status] NVARCHAR(50) NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([payment_id]),
    CONSTRAINT [FK_Payments_Bookings] FOREIGN KEY ([booking_id]) REFERENCES [dbo].[Bookings] ([booking_id]),
    CONSTRAINT [FK_Payments_Customers] FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customers] ([customer_id])
);
GO

ALTER TABLE [dbo].[Bookings]
ADD [payment_id] INT NULL,
CONSTRAINT [FK_Bookings_Payments] FOREIGN KEY ([payment_id]) REFERENCES [dbo].[Payments] ([payment_id]);

