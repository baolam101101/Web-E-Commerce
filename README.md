# 📌 Web E-commerce API

API cho hệ thống thương mại điện tử mini, xây dựng bằng ASP.NET Core.

---

## 📝 Mục tiêu dự án

- Xây dựng API hoàn chỉnh cho trang thương mại điện tử, bao gồm:
  - Quản lý sản phẩm, danh mục.
  - Quản lý người dùng, giỏ hàng, đơn hàng.
  - Xử lý thanh toán (giả lập).
- Học cách xây dựng RESTful API chuẩn, authentication với JWT, phân quyền user/admin.
- Thực hành Entity Framework Core, mapping quan hệ, repository pattern.

---

## 🛠️ Công nghệ sử dụng

- **Backend**: ASP.NET Core 8, Entity Framework Core
- **Database**: SQL Server
- **Authentication**: JWT
- **Môi trường phát triển**: Visual Studio Community 2022
- **Các thư viện chính**: AutoMapper, Swashbuckle (Swagger), FluentValidation (nếu có).

---

## 🗂️ Kiến trúc dự án

- **Models**: Định nghĩa entity như User, Product, Order, OrderItem, v.v.
- **Data**: AppDbContext quản lý database.
- **Repositories**: Interfaces & Implementations cho nghiệp vụ CRUD.
- **Controllers**: API endpoint.
- **DTOs**: Request/response models.
- **Middleware**: Xử lý lỗi, JWT auth (nếu có).

---

## 📦 Các chức năng chính đã hoàn thiện

- CRUD sản phẩm, danh mục
- Đăng ký/đăng nhập người dùng, JWT
- Quản lý giỏ hàng
- Đặt đơn hàng, quản lý trạng thái đơn hàng
- Tính toán tổng tiền đơn hàng
- API chuẩn REST, trả về kết quả thống nhất (response wrapper).

---

## 🚧 Các chức năng dự kiến phát triển thêm

- Tích hợp gửi mail khi đặt hàng thành công.
- Tích hợp cổng thanh toán giả lập.
- Viết unit test cho repository & controller.
- Hoàn thiện UI với React.

--- 
