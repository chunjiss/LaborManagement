# Quan Ly Lao 
1.	Các chức năng cơ bản của hệ thống
Qua quá trính nghiên cứu nhóm đã xây dựng được một hệ thống CSDL quản lý người lao động cơ bản đáp ứng được một số yêu cầu của như sau:
-	Quản lý nhân viên đầy đủ các thông tin cần thiết như: mã, họ, tên, ngày sinh, giới tính, địa chỉ thường trú và tạm trú
-	Theo dõi, cập nhật thường xuyên thông tin nhân viên, phòng bạn, chức vụ,… 
-	Tra cứu tím kiếm thông tin nhân viên, chức vụ, phòng bạn,…
-	Thêm, sửa, xóa thông tin nhân viên.
2.	Mô hình phân cấp chức năng
 
3.	Phát hiện thực thể
	TAI_KHOAN(MaTK, TenHienThi, TenDangNhap, MatKhau)
	PHAN_CONG(MaNV, MaCT, ThoiGian)
	CONG_TRINH(MaCT, TenCT, DiaDiem, NgayCP, NgayKC, NgayHTDK)
	NHAN_VIEN(MaNV, MaPB, MaCV, HoTen, Ngaysinh, GioiTinh, DiaChiThuongTru, DiaChiTamTru)
	PHONG_BAN(MaPB, TenPB)
	CHUC_VU(MaCV, TenCV)

