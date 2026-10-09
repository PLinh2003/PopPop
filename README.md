<div align="center">

![PopPop Logo](wwwroot/img/logo.png)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Version](https://img.shields.io/badge/.NET-10-512BD4?logo=.net)](https://dotnet.microsoft.com/)
![Status](https://img.shields.io/badge/Status-Active%20Development-brightgreen)

[Về dự án](#-về-dự-án) | [Cài đặt](#-cài-đặt) | [Hướng dẫn sử dụng](#-hướng-dẫn-sử-dụng) | [Cấu trúc dự án](#-cấu-trúc-dự-án)

</div>

---

## 📋 Về dự án

**PopPop** là một nền tảng thương mại điện tử được phát triển bằng **ASP.NET Core 10** với phối hợp giữa **Razor Pages** pattern để xây dựng giao diện người dùng. Dự án cung cấp các tính năng quản lý sản phẩm, giỏ hàng, đơn hàng, quản lý người dùng, và hơn thế nữa.

- 👥 **Quản lý người dùng**: Hỗ trợ đăng ký, đăng nhập, xác minh email, quản lý hồ sơ
- 🛍️ **Quản lý sản phẩm**: Danh sách sản phẩm, chi tiết sản phẩm, tìm kiếm, lọc theo thương hiệu và danh mục
- 🛒 **Giỏ hàng**: Thêm/xóa sản phẩm, cập nhật số lượng
- 📦 **Quản lý đơn hàng**: Theo dõi đơn hàng, lịch sử mua hàng
- 🔐 **Xác thực & Phân quyền**: Hỗ trợ ASP.NET Core Identity với phân quyền vai trò
- 💾 **Cơ sở dữ liệu**: SQL Server với Entity Framework Core
- 🎨 **Giao diện**: Responsive Bootstrap 5, thiết kế hiện đại

## 🚀 Cài đặt

### Yêu cầu tiên quyết

- **.NET 10 SDK** - [Tải xuống](https://dotnet.microsoft.com/download/dotnet/10.0)
- **SQL Server** - [Tải xuống](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) hoặc sử dụng SQL Server Express
- **Visual Studio 2022** (khuyến nghị) hoặc **Visual Studio Code**
- **Git**
- **Node.js** (tùy chọn, nếu cần hỗ trợ frontend tools)

### Các bước cài đặt

#### 1. Clone repository

```bash
git clone https://github.com/PLinh2003/PopPop.git
cd PopPop
```

#### 2. Cấu hình cơ sở dữ liệu

Mở file `appsettings.json` và cập nhật chuỗi kết nối SQL Server theo môi trường của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PopPopShop;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Ví dụ chuỗi kết nối:**
- **Localhost**: `Server=localhost;Database=PopPopShop;User Id=sa;Password=12345;TrustServerCertificate=True;`
- **Named Instance**: `Server=MACHINE_NAME\SQLEXPRESS;Database=PopPopShop;Integrated Security=true;TrustServerCertificate=True;`

#### 3. Cài đặt NuGet packages

Khôi phục tất cả các gói phụ thuộc:

```bash
dotnet restore
```

Hoặc sử dụng Package Manager trong Visual Studio:
```
Package Manager Console > Update-Package
```

#### 4. Áp dụng Migration để tạo cơ sở dữ liệu

```bash
dotnet ef database update
```

Hoặc sử dụng Package Manager Console:
```
Package Manager Console > Update-Database
```

#### 5. Chạy ứng dụng

```bash
dotnet run
```

Hoặc nhấn **F5** trong Visual Studio.

Ứng dụng sẽ chạy tại: `https://localhost:7136`

## 📖 Hướng dẫn sử dụng

### Đăng nhập

1. Truy cập trang chủ
2. Nhấp vào **Login** ở góc trên phải
3. Nhập email và mật khẩu
4. Nếu chưa có tài khoản, nhấp **Register** để tạo mới

### Duyệt sản phẩm

1. Trên trang chủ, chọn danh mục từ menu
2. Sử dụng bộ lọc để tìm sản phẩm theo:
   - Thương hiệu
   - Khoảng giá
   - Tình trạng hàng
3. Nhấp vào sản phẩm để xem chi tiết

### Xây dựng giỏ hàng

1. Vào trang chi tiết sản phẩm
2. Chọn số lượng
3. Nhấp **Add to Cart**
4. Tiếp tục mua sắm hoặc truy cập giỏ hàng

### Thanh toán

1. Truy cập **Cart**
2. Kiểm tra các mục trong giỏ
3. Nhấp **Proceed to Checkout**
4. Xác nhận thông tin giao hàng
5. Nhấp **Place Order**

### Quản lý tài khoản

1. Đăng nhập vào tài khoản của bạn
2. Truy cập **My Profile** hoặc **Account Settings**
3. Cập nhật:
   - Thông tin cá nhân
   - Mật khẩu
   - Email
   - Cài đặt bảo mật (2FA)

### Truy cập Admin Dashboard

1. Đăng nhập với tài khoản Admin
2. Truy cập `https://localhost:7136/admin/dashboard`
3. Quản lý sản phẩm, danh mục, người dùng, đơn hàng

## 📁 Cấu trúc dự án

```
PopPop/
├── Areas/
│   ├── Admin/                      # Khu vực quản trị
│   │   ├── Controllers/
│   │   │   └── DashboardController.cs
│   │   └── Views/
│   │       └── Dashboard/
│   └── Identity/                   # Khu vực Xác thực & Phân quyền
│       └── Pages/
│           └── Account/            # Các trang đăng nhập, đăng ký, v.v.
│               ├── Login.cshtml
│               ├── Register.cshtml
│               ├── Logout.cshtml
│               └── Manage/         # Quản lý tài khoản
├── Controllers/                    # Controllers cho các trang chính
│   └── HomeController.cs
├── Data/                           # Dữ liệu & Cơ sở dữ liệu
│   ├── Entities/                   # Entity Models
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── Product.cs
│   │   ├── ProductVariant.cs
│   │   ├── Category.cs
│   │   ├── Brand.cs
│   │   ├── Cart.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── InventoryTransaction.cs
│   │   └── Enums/
│   │       ├── OrderStatus.cs
│   │       └── InventoryTransactionType.cs
│   ├── Migrations/                 # Database Migrations
│   └── PopPopDbContext.cs          # DbContext chính
├── Models/                         # ViewModel & Models
│   └── ErrorViewModel.cs
├── Pages/                          # Razor Pages
├── Services/                       # Business Logic
│   ├── Interfaces/
│   │   ├── IService.cs
│   │   └── IProductService.cs
│   └── Implementations/
│       └── ProductService.cs
├── Repositories/                   # Data Access Layer
│   └── Interfaces/
│       └── IRepository.cs
├── Views/                          # MVC Views
│   ├── Home/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   ├── _Header.cshtml
│   │   ├── _Footer.cshtml
│   │   └── _LoginPartial.cshtml
│   └── _ViewImports.cshtml
├── wwwroot/                        # Tài nguyên tĩnh
│   ├── css/                        # Stylesheets
│   ├── js/                         # JavaScript files
│   ├── img/                        # Hình ảnh
│   ├── lib/                        # Thư viện client
│   ├── fonts/                      # Font files
│   └── scss/                       # SCSS source files
├── Doc/                            # Tài liệu
│   ├── Convention.md               # Quy ước mã hóa
│   └── FrontEnd/Ui.md              # Hướng dẫn UI
├── Properties/
│   ├── launchSettings.json
│   └── serviceDependencies.json
├── appsettings.json                # Cấu hình ứng dụng
├── Program.cs                      # Entry point
├── PopPop.csproj                   # Project file
├── PopPop.slnx                     # Solution file
└── README.md                       # This file
```

## 🔧 Cấu hình

### appsettings.json

Các cài đặt chính:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=PopPopShop;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Tạo tài khoản Admin (Seed Data)

Sau khi chạy ứng dụng, bạn có thể tạo tài khoản Admin bằng cách:

1. Sử dụng Database > appsettings.json seed data
2. Hoặc trên **Package Manager Console**:
   ```
   Update-Database -Seed
   ```

## 🗄️ Database Schema

### Các bảng chính:

| Bảng | Mô tả |
|------|-------|
| `Users` | Thông tin người dùng |
| `Roles` | Vai trò người dùng |
| `Products` | Sản phẩm |
| `ProductVariants` | Biến thể sản phẩm (kích cỡ, màu sắc, v.v.) |
| `ProductImages` | Hình ảnh sản phẩm |
| `Categories` | Danh mục sản phẩm |
| `Brands` | Thương hiệu |
| `Carts` | Giỏ hàng của người dùng |
| `CartItems` | Mục trong giỏ hàng |
| `Orders` | Đơn hàng |
| `OrderItems` | Mục trong đơn hàng |
| `InventoryTransactions` | Lịch sử giao dịch kho hàng |

## 🔐 Bảo mật

- ✅ Sử dụng **ASP.NET Core Identity** để quản lý người dùng
- ✅ Hỗ trợ **2-Factor Authentication (2FA)**
- ✅ Xác thực email bắt buộc
- ✅ Mã hóa mật khẩu với **bcrypt**
- ✅ HTTPS được kích hoạt
- ✅ Token-based authentication (JWT ready)
- ✅ CSRF protection

## 📦 Các công nghệ được sử dụng

### Backend
- **ASP.NET Core 10** - Framework chính
- **Entity Framework Core** - ORM
- **SQL Server** - Cơ sở dữ liệu
- **ASP.NET Core Identity** - Xác thực & Phân quyền
- **Swagger/OpenAPI** - API Documentation

### Frontend
- **Razor Pages & MVC** - Rendering HTML
- **Bootstrap 5** - CSS Framework
- **jQuery** - JavaScript utility
- **Font Awesome** - Icons
- **Owl Carousel** - Product carousel
- **Tempusdominus** - Date/Time picker

### Công cụ phát triển
- **Visual Studio 2022**
- **SQL Server Management Studio**
- **Git & GitHub**
- **.NET CLI**

## 🚀 Xây dựng & Triển khai

### Build Release

```bash
dotnet build --configuration Release
```

### Publish

```bash
dotnet publish -c Release -o ./publish
```

### Chạy phiên bản Release

```bash
dotnet PopPop.dll
```

### Docker (Tùy chọn)

Để chạy với Docker, tạo `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "PopPop.dll"]
```

Build và chạy:
```bash
docker build -t poppop:latest .
docker run -p 5000:80 poppop:latest
```

## 🐛 Troubleshooting

### 1. **Migration Error**
```
Update-Database: No migrations found
```
**Giải pháp:**
```bash
Add-Migration InitialCreate
Update-Database
```

### 2. **Connection String Error**
```
Connection string 'DefaultConnection' not found
```
**Giải pháp:** Kiểm tra `appsettings.json` và chuỗi kết nối SQL Server

### 3. **Port Already in Use**
**Giải pháp:** Thay đổi port trong `launchSettings.json`:
```json
"urls": "https://localhost:7137"
```

### 4. **HTTPS Certificate Error**
```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

## 📚 Tài liệu bổ sung

- [Quy ước code](Doc/Convention.md)
- [Hướng dẫn UI/Frontend](Doc/FrontEnd/Ui.md)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/)

## 👨‍💻 Đóng góp

Chúng tôi rất hoan nghênh các đóng góp! Để bắt đầu:

1. Fork repository
2. Tạo branch feature (`git checkout -b feature/AmazingFeature`)
3. Commit các thay đổi (`git commit -m 'Add some AmazingFeature'`)
4. Push đến branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

### Hướng dẫn đóng góp
- Tuân theo [Quy ước code](Doc/Convention.md)
- Viết commit message rõ ràng và mô tả
- Thêm test nếu cần thiết
- Cập nhật tài liệu nếu có thay đổi public API

## 📄 Giấy phép

Dự án này được cấp phép dưới **License MIT** - xem file [LICENSE](LICENSE) để biết chi tiết.

## 📞 Liên hệ & Hỗ trợ

- **Repository:** [github.com/PLinh2003/PopPop](https://github.com/PLinh2003/PopPop)
- **Issues:** [Report bugs & request features](https://github.com/PLinh2003/PopPop/issues)
- **Email:** [Contact the maintainers]

## 🎯 Roadmap

- [ ] Tích hợp Payment Gateway (Stripe, VNPay)
- [ ] Multi-language support (i18n)
- [ ] Mobile app (React Native / Flutter)
- [ ] Real-time notifications (SignalR)
- [ ] Advanced reporting & analytics
- [ ] Machine Learning recommendations
- [ ] Microservices architecture
- [ ] Performance optimization & caching

## 👏 Cảm ơn

Cảm ơn tất cả những người đã đóng góp vào dự án này!

---

<div align="center">

**[⬆ Quay lại đầu trang](#-nền-tảng-thương-mại-điện-tử)**

Made with ❤️ by TuanVu

</div>