create database QuanLyLaoDong
go

use QuanLyLaoDong
go

create table CONGTRINH
(
	MaCT varchar(30) primary key,
	TenCT nvarchar(100),
	DiaDiem nvarchar(500),
	NgayCP datetime,
	NgayKC datetime,
	NgayHTDK datetime
)
go

create table PHONGBAN
(
	MaPB varchar(20) primary key,
	TenPB nvarchar(50)
)
go
create table CHUCVU
(
	MaCV varchar(20) primary key,
	TenCV nvarchar(50)
)
go
create table NHANVIEN
(
	MaNV varchar(20) primary key,
	MaPB varchar(20),
	MaCV varchar(20),
	HoTen nvarchar(50) not null,
	NgaySinh datetime,
	GioiTinh nvarchar(5),
	DiaChiThuongTru nvarchar(500),
	DiaChiTamTru nvarchar(500),
	foreign key (MaPB) references dbo.PHONGBAN(MaPB),
	foreign key (MaCV) references dbo.CHUCVU(MaCV)
)
go
create table PHANCONG
(
	MaNV varchar(20),
	MaCT varchar(30),
	ThoiGian int not null,
	foreign key (MaNV) references dbo.NHANVIEN(MaNV),
	foreign key (MaCT) references dbo.CONGTRINH(MaCT)
)
go
create table TAIKHOAN
(
	MaTK varchar(20) primary key,
	TenHienThi nvarchar(50),
	TenDangNhap varchar(30),
	MatKhau varchar(20)
)