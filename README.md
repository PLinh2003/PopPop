# PopPop
<div align="center">

![PopPop Logo](wwwroot/img/logo.png)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Version](https://img.shields.io/badge/.NET-10-512BD4?logo=.net)](https://dotnet.microsoft.com/)
![Status](https://img.shields.io/badge/Status-Active%20Development-brightgreen)

[Về dự án](#-về-dự-án) | [Cài đặt](#-cài-đặt) | [Cấu trúc dự án](#-cấu-trúc-dự-án) | [Luồng hoạt động](#-luồng-hoạt-động-các-tầng-nghiệp-vụ) | [Đóng góp](#-đóng-góp) | [Liên hệ](#-liên-hệ) | [Giấy phép](#-giấy-phép)

</div>

---

## 📋 Về dự án

**PopPop** là dự án được phát triển cho đề tài khóa luận tốt nghiệp **Xây dựng hệ thống website quản lý và bán hàng trực tuyến cho cửa hàng cầu lông PôpPôp**, dự án được xây dựng nhằm mục đích học tập và nghiên cứu sát với thực tế, và không phục vụ mục đích thương mại.

## 🚀 Cài đặt

### Yêu cầu tiên quyết

- **.NET 10 SDK** - [Tải xuống](https://dotnet.microsoft.com/download/dotnet/10.0)
- **SQL Server** - [Tải xuống](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)(hoặc `database` khác nhưng phải tự cấu hình)

### Các bước cài đặt

#### 1. Clone repository

```bash
git clone https://github.com/PLinh2003/PopPop.git
cd PopPop
```

#### 2. Cấu hình cơ sở dữ liệu

Tạo file `appsettings.json` và cập nhật `"DefaultConnection"` SQL Server theo môi trường của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=[YOUR_SERVER_NAME];Database=[YOUR_DATABASE_NAME];User Id=[YOUR_USER_ID];Password=[YOUR_PASSWORD];TrustServerCertificate=True;"
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
Có thể tạo luôn file `appsettings.json` bằng lệnh trong `terminal`
```pwshell
@'
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=[YOUR_SERVER_NAME];Database=[YOUR_DATABASE_NAME];User Id=[YOUR_USER_ID];Password=[YOUR_PASSWORD];TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
'@ | Set-Content -Path "appsettings.json" -Encoding utf8
```

#### 3. Cài đặt NuGet packages

Khôi phục tất cả các gói phụ thuộc:

```pwshell
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

## 📁 Cấu trúc dự án

```
PopPop/
├── Areas/
│   ├── Admin/                      # Khu vực quản trị
│   ├── Staff/                      # Khu vực nhân viên
│   └── Identity/                   # Khu vực Xác thực & Phân quyền
├── Controllers/                    # Controllers cho các trang chính và khách hàng
├── Data/                           # Dữ liệu & Cơ sở dữ liệu
│   ├── Entities/                   # Thực thể cơ sở dữ liệu
├── Models/                         # DTO request và response
├── Services/                       # Business Logic
├── Repositories/                   # Tầng truy vấn vào cơ sở dữ liệu
├── Views/                          # UI
├── wwwroot/                        # Tài nguyên tĩnh
├── Doc/                            # Tài liệu
├── appsettings.json                # Cấu hình ứng dụng
├── Program.cs                      # Entry point
```

## 🔄 Luồng hoạt động các tầng nghiệp vụ

Dự án PopPop sử dụng **N-Tier Architecture** với 5 tầng chính:

```
📱 Views/Razor Pages (Presentation)
         ↓ (HTTP Request)
🎮 Controllers (Application Layer)
         ↓ (Service Call)
💼 Services (Business Logic)
         ↓ (Repository Call)
📊 Repositories (Data Access)
         ↓ (EF Core Query)
🗄️  SQL Server (Database)
```

| Tầng | Vai trò | Công nghệ |
|------|---------|-----------|
| 🎨 Presentation | Hiển thị UI, nhận input từ user | Views, Pages |
| 🎮 Application | Điều phối request, validate dữ liệu | Controllers |
| 💼 Business Logic | Xử lý logic nghiệp vụ, business rules | Services |
| 📊 Data Access | CRUD, query database | Repositories |
| 🗄️ Database | Lưu trữ dữ liệu | SQL Server |

## 👨‍💻 Đóng góp

Chúng tôi rất hoan nghênh các đóng góp!

1. Fork repository và Pull request
2. Liên hệ với chung tôi nếu bạn muốn tham gia vào Repo

## 📞 Liên hệ
- **Email:** `linhphuonglinh0321@gmail.com`

## 📄 Giấy phép

Dự án này được cấp phép dưới **License MIT** - xem file [LICENSE](LICENSE) để biết chi tiết.
## 👏 Cảm ơn

Cảm ơn tất cả những người đã đóng góp vào dự án này!

---

<div align="center">

**[⬆ Quay lại đầu trang](#poppop)**

Made with ❤️ by TuanVu

</div>
