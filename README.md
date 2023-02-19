# I. Phân tích yêu cầu bài toán và thiết kế database
## 1.	Các chức năng cơ bản của hệ thống
Qua quá trính nghiên cứu nhóm đã xây dựng được một hệ thống CSDL quản lý người lao động cơ bản đáp ứng được một số yêu cầu của như sau:
-	Quản lý nhân viên đầy đủ các thông tin cần thiết như: mã, họ, tên, ngày sinh, giới tính, địa chỉ thường trú và tạm trú
-	Theo dõi, cập nhật thường xuyên thông tin nhân viên, phòng bạn, chức vụ,… 
-	Tra cứu tím kiếm thông tin nhân viên, chức vụ, phòng bạn,…
-	Thêm, sửa, xóa thông tin nhân viên.
## 2.	Mô hình phân cấp chức năng
 ![image](https://user-images.githubusercontent.com/80302795/219974189-2641b165-5d7e-4cc9-8bc8-7064d9914643.png)
 
## 3.	Phát hiện thực thể
	TAI_KHOAN(MaTK, TenHienThi, TenDangNhap, MatKhau)
	PHAN_CONG(MaNV, MaCT, ThoiGian)
	CONG_TRINH(MaCT, TenCT, DiaDiem, NgayCP, NgayKC, NgayHTDK)
	NHAN_VIEN(MaNV, MaPB, MaCV, HoTen, Ngaysinh, GioiTinh, DiaChiThuongTru, DiaChiTamTru)
	PHONG_BAN(MaPB, TenPB)
	CHUC_VU(MaCV, TenCV)

