# CHƯƠNG I. PHÂN TÍCH YÊU CẦU BÀI TOÁN VÀ THIẾT KẾ DATABASE
## 1.	Các chức năng cơ bản của hệ thống
Qua quá trính nghiên cứu nhóm đã xây dựng được một hệ thống CSDL quản lý người lao động cơ bản đáp ứng được một số yêu cầu của như sau:
-	Quản lý nhân viên đầy đủ các thông tin cần thiết như: mã, họ, tên, ngày sinh, giới tính, địa chỉ thường trú và tạm trú
-	Theo dõi, cập nhật thường xuyên thông tin nhân viên, phòng bạn, chức vụ,… 
-	Tra cứu tím kiếm thông tin nhân viên, chức vụ, phòng bạn,…
-	Thêm, sửa, xóa thông tin nhân viên.
## 2.	Mô hình phân cấp chức năng
 ![image](https://user-images.githubusercontent.com/80302795/219974189-2641b165-5d7e-4cc9-8bc8-7064d9914643.png)
 
## 3.	Phát hiện thực thể
- TAI_KHOAN(MaTK, TenHienThi, TenDangNhap, MatKhau)
- PHAN_CONG(MaNV, MaCT, ThoiGian)
- CONG_TRINH(MaCT, TenCT, DiaDiem, NgayCP, NgayKC, NgayHTDK)
- NHAN_VIEN(MaNV, MaPB, MaCV, HoTen, Ngaysinh, GioiTinh, DiaChiThuongTru, DiaChiTamTru)
- PHONG_BAN(MaPB, TenPB)
- CHUC_VU(MaCV, TenCV)

## 4.	Mô hình ERD
![image](https://user-images.githubusercontent.com/80302795/219974357-47642ced-647e-47de-b473-61e9673f5f7e.png)

## 5.	Mô hình quan hệ
![image](https://user-images.githubusercontent.com/80302795/219974358-cef94ada-6bac-4bcc-91e2-e7627fa0209c.png)

# II.Ý tưởng thiết kế 
Dựa trên những phân tích yêu cầu bài toán ở trên, nhóm đưa ra ý tưởng thiết kế các chức năng như sau:
- Đầu tiên là phần đăng nhập, nếu người dùng nhập sai tài khoản hoặc mật khẩu thì thông báo lỗi và để người dùng nhập lại mật khẩu. Ngược lại, chuyển người dùng đến trang quản trị.
![image](https://user-images.githubusercontent.com/80302795/219974439-415c9302-2f8f-4199-a01f-612d549b2a3e.png)
- Sau khi được chuyển đến trang quản trị, tại đây người dùng có thể thực hiện quản lý thông tin nhân viên, chức vụ, phòng ban, phân công, công trình, tài khoản mật khẩu.
![image](https://user-images.githubusercontent.com/80302795/219974446-329aae66-b160-4648-869f-c663ecdcf681.png)
•	Khi người dùng chọn vào chức năng quản lý nhân viên thì sẽ hiển thị danh sách nhân viên và các chức năng thêm, sửa, xóa tương ứng. 
-	Khi muốn thêm mới thì người dùng phải nhập mã nhân viên không được trùng với mã đã có và tên nhân viên không được để trống, nếu không thì không được phép thêm. 
-	Khi muốn sửa thì người dùng phải chọn vào đối tượng cần sửa, khi đó sẽ hiển thị thông tin cần sửa, sau khi sửa người dùng có thể lưu thay đổi hoặc hủy bỏ thao tác. Ngược lại nếu không chọn đối tượng cần sửa thì không có thông tin để sửa.
-	Khi muốn xóa thì người dùng phải chọn đối tượng cần xóa, khi đó sẽ hiển thị thông báo xác nhận xóa, người dùng có thể chọn xóa hoặc hủy thao tác. Ngược lại, nếu không chọn đối tượng thì không có thông tin để xóa.
![image](https://user-images.githubusercontent.com/80302795/219974457-b6e68f37-50da-47a4-961e-e58b8bc5d8d9.png)
•	Khi người dùng chọn vào chức năng quản lý phòng ban thì sẽ hiển thị danh sách phòng ban và các chức năng thêm, sửa, xóa tương ứng. 
-	Khi muốn thêm mới thì người dùng phải nhập mã phòng ban không được trùng với mã đã có và tên phòng ban không được để trống, nếu không thì không được phép thêm. 
-	Khi muốn sửa thì người dùng phải chọn vào đối tượng cần sửa, khi đó sẽ hiển thị thông tin cần sửa, sau khi sửa người dùng có thể lưu thay đổi hoặc hủy bỏ thao tác. Ngược lại nếu không chọn đối tượng cần sửa thì không có thông tin để sửa.
-	Khi muốn xóa thì người dùng phải chọn đối tượng cần xóa, khi đó sẽ hiển thị thông báo xác nhận xóa, người dùng có thể chọn xóa hoặc hủy thao tác. Ngược lại, nếu không chọn đối tượng thì không có thông tin để xóa.
![image](https://user-images.githubusercontent.com/80302795/219974474-bb656933-4880-493a-818d-62fdcc126f9e.png)
•	Khi người dùng chọn vào chức năng quản lý công trình thì sẽ hiển thị danh sách công trình và các chức năng thêm, sửa, xóa tương ứng. 
-	Khi muốn thêm mới thì người dùng phải nhập mã công trình không được trùng với mã đã có và tên công trình không được để trống, nếu không thì không được phép thêm. 
-	Khi muốn sửa thì người dùng phải chọn vào đối tượng cần sửa, khi đó sẽ hiển thị thông tin cần sửa, sau khi sửa người dùng có thể lưu thay đổi hoặc hủy bỏ thao tác. Ngược lại nếu không chọn đối tượng cần sửa thì không có thông tin để sửa.
-	Khi muốn xóa thì người dùng phải chọn đối tượng cần xóa, khi đó sẽ hiển thị thông báo xác nhận xóa, người dùng có thể chọn xóa hoặc hủy thao tác. Ngược lại, nếu không chọn đối tượng thì không có thông tin để xóa.
![image](https://user-images.githubusercontent.com/80302795/219974493-ff23a688-b4f2-4e6f-bd09-17039a1dbee6.png)
•	Khi người dùng chọn vào chức năng phân công thì sẽ hiển thị danh sách phân công và các chức năng thêm, sửa, xóa tương ứng. 
-	Khi muốn thêm mới thì người dùng phải nhập mã nhân viên và mã công trình không được trùng với mã đã có và thời gian không được để trống, nếu không thì không được phép thêm. 
-	Khi muốn sửa thì người dùng phải chọn vào đối tượng cần sửa, khi đó sẽ hiển thị thông tin cần sửa, sau khi sửa người dùng có thể lưu thay đổi hoặc hủy bỏ thao tác. Ngược lại nếu không chọn đối tượng cần sửa thì không có thông tin để sửa.
-	Khi muốn xóa thì người dùng phải chọn đối tượng cần xóa, khi đó sẽ hiển thị thông báo xác nhận xóa, người dùng có thể chọn xóa hoặc hủy thao tác. Ngược lại, nếu không chọn đối tượng thì không có thông tin để xóa.
![image](https://user-images.githubusercontent.com/80302795/219974502-56b2f280-747b-431f-b2c5-1c2dbe79d88c.png)
•	Khi người dùng chọn vào chức năng quản lý tài khoản thì sẽ hiển thị danh sách phân công và các chức năng thêm, sửa, xóa tương ứng. 
-	Khi muốn thêm mới thì người dùng phải nhập mã tài khoản và mã tài khoản không được trùng với mã đã có và tên hiển thị, tên đăng nhập, mật khẩu không được để trống, nếu không thì không được phép thêm. 
-	Khi muốn sửa thì người dùng phải chọn vào đối tượng cần sửa, khi đó sẽ hiển thị thông tin cần sửa, sau khi sửa người dùng có thể lưu thay đổi hoặc hủy bỏ thao tác. Ngược lại nếu không chọn đối tượng cần sửa thì không có thông tin để sửa.
-	Khi muốn xóa thì người dùng phải chọn đối tượng cần xóa, khi đó sẽ hiển thị thông báo xác nhận xóa, người dùng có thể chọn xóa hoặc hủy thao tác. Ngược lại, nếu không chọn đối tượng thì không có thông tin để xóa.
![image](https://user-images.githubusercontent.com/80302795/219974516-222e60af-5d91-4a90-a41e-c2a43c309559.png)
## CHƯƠNG II: THIẾT KẾ GIAO DIỆN VÀ CHỨC NĂNG
# 1.	Khu vực menu
![image](https://user-images.githubusercontent.com/80302795/219974614-8a26e963-57d4-4f3c-b283-56a3cb99fb23.png)

Đây là phần menu của ứng dụng, có các chức năng giúp người dùng đến các trang quản lý công trình, nhân viên, phòng ban, chức vụ, phân công và tài khoản đăng nhập. Ngoài ra, phần trên cùng là tên của phần mềm.
## 2. Khu vực điều hướng
![image](https://user-images.githubusercontent.com/80302795/219974644-688b3cd1-135f-46dd-9ea4-d7d9f45dc9a9.png)

Đây là khu vực mà người dùng có thể thực hiện các thao tác ẩn hiện phần menu, tìm kiếm và có một vài nút trợ giúp để cho người xem thông báo, thông tin tài khoản. Sơ đồ như sau:
![image](https://user-images.githubusercontent.com/80302795/219974661-c7852170-1938-44ee-b378-8f52731b04df.png)
## 3. Giao diện trang chủ
![image](https://user-images.githubusercontent.com/80302795/219974680-961b4b8e-bd87-408f-b3a1-0dc05c6d3117.png)

Đây là giao diện khi người dùng đăng nhập thành công, người dùng sẽ được đưa đến giao diện như hình trên.
# KẾT LUẬN
Thông qua việc phân tích những yêu cầu của bài toán, cũng như dựa trên những đặc tả của dữ liệu em đã hoàn thành thiết kế database cho bài toán, thiết kế giao diện cho phần mềm quản lí lao động. Các chức năng chính của hệ thống như yêu cầu đã đề ra như: các chức năng thêm, sửa xóa, tìm kiếm dữ liệu.
Bên cạnh những gì đã làm được, phần nghiên cứu của nhóm vẫn tồn tại những điểm hạn chế như database mới dừng ở mức cơ bản của việc quản lí lao động, giao diện chưa được đẹp như kì vọng,… vì vậy trong thời gian tới nhóm mong rằng sẽ có những điều chỉnh để cho sản phẩm hoàn thiện hơn, đáp ứng được những yêu cầu cần phải có của một phần mềm quản lí, giao diện đẹp và thân thiện với người dùng hơn.








