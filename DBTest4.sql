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




----------------------

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2023 23:46:04
-- Generated from EDMX file: C:\Users\Mr.Vien\source\repos\MayBay\MayBay\Models\BookingAirLine.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookingAirLine];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Bookings_ChuyenBays]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_Bookings_ChuyenBays];
GO
IF OBJECT_ID(N'[dbo].[FK_Bookings_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_Bookings_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Bookings_Guest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_Bookings_Guest];
GO
IF OBJECT_ID(N'[dbo].[FK_Bookings_Payments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_Bookings_Payments];
GO
IF OBJECT_ID(N'[dbo].[FK_ChuyenBays_HangHangKhong]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChuyenBays] DROP CONSTRAINT [FK_ChuyenBays_HangHangKhong];
GO
IF OBJECT_ID(N'[dbo].[FK_ChuyenBays_TuyenBays]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChuyenBays] DROP CONSTRAINT [FK_ChuyenBays_TuyenBays];
GO
IF OBJECT_ID(N'[dbo].[FK_Payments_Bookings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_Payments_Bookings];
GO
IF OBJECT_ID(N'[dbo].[FK_Payments_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_Payments_Customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[ChuyenBays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChuyenBays];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Guest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guest];
GO
IF OBJECT_ID(N'[dbo].[HangHangKhongs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HangHangKhongs];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[TuyenBays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TuyenBays];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [username] nvarchar(50)  NULL,
    [password] nvarchar(50)  NULL,
    [customer_id] int  NOT NULL,
    [is_admin] bit  NULL,
    [HoTen] nvarchar(max)  NULL,
    [NgaySinh] datetime  NULL,
    [Email] nvarchar(max)  NULL,
    [SoDienThoai] nvarchar(max)  NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [booking_id] int IDENTITY(1,1) NOT NULL,
    [MaCB] int  NULL,
    [customer_id] int  NULL,
    [ngay_bay] datetime  NULL,
    [trang_thai] nvarchar(50)  NULL,
    [payment_id] int  NULL,
    [Id_Guest] int  NULL
);
GO

-- Creating table 'ChuyenBays'
CREATE TABLE [dbo].[ChuyenBays] (
    [MaCB] int IDENTITY(1,1) NOT NULL,
    [MaTBay] int  NULL,
    [MaHangHang] int  NULL,
    [NgayGio] datetime  NULL,
    [Gia] decimal(18,2)  NULL,
    [ThoiGianBay] int  NULL,
    [SoLuongGheHang1] int  NULL,
    [SoLuongGheHang2] int  NULL,
    [SoLuongGheHang3] int  NULL,
    [TinhTrang] nvarchar(50)  NULL,
    [HinhAnh] nvarchar(100)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [customer_id] int IDENTITY(1,1) NOT NULL,
    [full_name] nvarchar(50)  NULL,
    [phone_number] nvarchar(20)  NULL,
    [email] nvarchar(50)  NULL,
    [NgaySinh] datetime  NULL
);
GO

-- Creating table 'Guests'
CREATE TABLE [dbo].[Guests] (
    [Id_Guest] int IDENTITY(1,1) NOT NULL,
    [HoTenG] nvarchar(max)  NULL,
    [EmailG] nvarchar(max)  NULL,
    [SoDienThoaiG] nvarchar(max)  NULL,
    [NgaySinhG] datetime  NULL,
    [SoHoChieu] nvarchar(max)  NULL,
    [QuocGiaCap] nvarchar(max)  NULL,
    [NgayHetHan] datetime  NULL
);
GO

-- Creating table 'HangHangKhongs'
CREATE TABLE [dbo].[HangHangKhongs] (
    [MaHangHang] int IDENTITY(1,1) NOT NULL,
    [HinhAnh] varchar(255)  NULL,
    [TenHH] varchar(255)  NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [payment_id] int IDENTITY(1,1) NOT NULL,
    [booking_id] int  NULL,
    [payment_date] datetime  NULL,
    [payment_method] nvarchar(50)  NULL,
    [amount] decimal(10,2)  NULL,
    [customer_id] int  NULL,
    [payment_status] nvarchar(50)  NULL
);
GO

-- Creating table 'TuyenBays'
CREATE TABLE [dbo].[TuyenBays] (
    [MaTBay] int IDENTITY(1,1) NOT NULL,
    [SanBayDi] nvarchar(50)  NULL,
    [SanBayDen] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [customer_id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([customer_id] ASC);
GO

-- Creating primary key on [booking_id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([booking_id] ASC);
GO

-- Creating primary key on [MaCB] in table 'ChuyenBays'
ALTER TABLE [dbo].[ChuyenBays]
ADD CONSTRAINT [PK_ChuyenBays]
    PRIMARY KEY CLUSTERED ([MaCB] ASC);
GO

-- Creating primary key on [customer_id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([customer_id] ASC);
GO

-- Creating primary key on [Id_Guest] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [PK_Guests]
    PRIMARY KEY CLUSTERED ([Id_Guest] ASC);
GO

-- Creating primary key on [MaHangHang] in table 'HangHangKhongs'
ALTER TABLE [dbo].[HangHangKhongs]
ADD CONSTRAINT [PK_HangHangKhongs]
    PRIMARY KEY CLUSTERED ([MaHangHang] ASC);
GO

-- Creating primary key on [payment_id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([payment_id] ASC);
GO

-- Creating primary key on [MaTBay] in table 'TuyenBays'
ALTER TABLE [dbo].[TuyenBays]
ADD CONSTRAINT [PK_TuyenBays]
    PRIMARY KEY CLUSTERED ([MaTBay] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MaCB] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Bookings_ChuyenBays]
    FOREIGN KEY ([MaCB])
    REFERENCES [dbo].[ChuyenBays]
        ([MaCB])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bookings_ChuyenBays'
CREATE INDEX [IX_FK_Bookings_ChuyenBays]
ON [dbo].[Bookings]
    ([MaCB]);
GO

-- Creating foreign key on [customer_id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Bookings_Customers]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[Customers]
        ([customer_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bookings_Customers'
CREATE INDEX [IX_FK_Bookings_Customers]
ON [dbo].[Bookings]
    ([customer_id]);
GO

-- Creating foreign key on [Id_Guest] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Bookings_Guest]
    FOREIGN KEY ([Id_Guest])
    REFERENCES [dbo].[Guests]
        ([Id_Guest])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bookings_Guest'
CREATE INDEX [IX_FK_Bookings_Guest]
ON [dbo].[Bookings]
    ([Id_Guest]);
GO

-- Creating foreign key on [payment_id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Bookings_Payments]
    FOREIGN KEY ([payment_id])
    REFERENCES [dbo].[Payments]
        ([payment_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bookings_Payments'
CREATE INDEX [IX_FK_Bookings_Payments]
ON [dbo].[Bookings]
    ([payment_id]);
GO

-- Creating foreign key on [booking_id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_Payments_Bookings]
    FOREIGN KEY ([booking_id])
    REFERENCES [dbo].[Bookings]
        ([booking_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Payments_Bookings'
CREATE INDEX [IX_FK_Payments_Bookings]
ON [dbo].[Payments]
    ([booking_id]);
GO

-- Creating foreign key on [MaHangHang] in table 'ChuyenBays'
ALTER TABLE [dbo].[ChuyenBays]
ADD CONSTRAINT [FK_ChuyenBays_HangHangKhong]
    FOREIGN KEY ([MaHangHang])
    REFERENCES [dbo].[HangHangKhongs]
        ([MaHangHang])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChuyenBays_HangHangKhong'
CREATE INDEX [IX_FK_ChuyenBays_HangHangKhong]
ON [dbo].[ChuyenBays]
    ([MaHangHang]);
GO

-- Creating foreign key on [MaTBay] in table 'ChuyenBays'
ALTER TABLE [dbo].[ChuyenBays]
ADD CONSTRAINT [FK_ChuyenBays_TuyenBays]
    FOREIGN KEY ([MaTBay])
    REFERENCES [dbo].[TuyenBays]
        ([MaTBay])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChuyenBays_TuyenBays'
CREATE INDEX [IX_FK_ChuyenBays_TuyenBays]
ON [dbo].[ChuyenBays]
    ([MaTBay]);
GO

-- Creating foreign key on [customer_id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_Payments_Customers]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[Customers]
        ([customer_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Payments_Customers'
CREATE INDEX [IX_FK_Payments_Customers]
ON [dbo].[Payments]
    ([customer_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------