using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PopPop.Data.Entities;

namespace PopPop.Data
{
    public class PopPopDbContext : IdentityDbContext<User, Role, string>
    {
        // Static datetime for seed data to avoid EF Core warnings
        private static readonly DateTime _seedDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public PopPopDbContext(DbContextOptions<PopPopDbContext> options) 
            : base(options)
        {
        }

        #region DbSets for Business Entities

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductVariant> ProductVariants { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Category Configuration

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            #endregion

            #region Brand Configuration

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            #endregion

            #region Product Configuration

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.Property(e => e.Description)
                    .HasMaxLength(2000);

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Brand)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region ProductVariant Configuration

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SKU)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.VariantLabel)
                    .HasMaxLength(100);
                entity.HasIndex(e => e.SKU)
                    .IsUnique();

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);
                entity.Property(e => e.StockQuantity)
                    .HasDefaultValue(0);

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.ProductVariants)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region ProductImage Configuration

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.ProductImages)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Cart Configuration

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.UserId)
                    .IsUnique();
            });

            #endregion

            #region CartItem Configuration

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice)
                    .HasPrecision(18, 2);
                entity.Property(e => e.Quantity)
                    .HasDefaultValue(1);

                entity.HasOne(e => e.Cart)
                    .WithMany(e => e.CartItems)
                    .HasForeignKey(e => e.CartId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ProductVariant)
                    .WithMany(e => e.CartItems)
                    .HasForeignKey(e => e.ProductVariantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region Order Configuration

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.Property(e => e.OrderCode)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasIndex(e => e.OrderCode)
                    .IsUnique();

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(18, 2);
                entity.Property(e => e.ShippingFee)
                    .HasPrecision(18, 2);
                entity.Property(e => e.RecipientName)
                    .HasMaxLength(256);
                entity.Property(e => e.RecipientPhone)
                    .HasMaxLength(20);
                entity.Property(e => e.ShippingAddress)
                    .HasMaxLength(500);
                entity.Property(e => e.Note)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region OrderItem Configuration

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.Property(e => e.SKU)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.UnitPrice)
                    .HasPrecision(18, 2);
                entity.Property(e => e.TotalPrice)
                    .HasPrecision(18, 2);

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ProductVariant)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ProductVariantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region InventoryTransaction Configuration

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Note)
                    .HasMaxLength(500);

                entity.HasOne(e => e.ProductVariant)
                    .WithMany(e => e.InventoryTransactions)
                    .HasForeignKey(e => e.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Seed Data

            SeedRoles(modelBuilder);
            SeedCategories(modelBuilder);
            SeedBrands(modelBuilder);
            SeedProducts(modelBuilder);
            SeedProductVariants(modelBuilder);
            SeedProductImages(modelBuilder);
            SeedAdmin(modelBuilder);

            #endregion
        }

        /// <summary>
        /// Seed default roles
        /// </summary>
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Administrator with full system access"
                },
                new Role
                {
                    Id = "2",
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    Description = "Staff member for shop operations"
                },
                new Role
                {
                    Id = "3",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    Description = "Regular customer"
                }
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }

        /// <summary>
        /// Seed default categories
        /// </summary>
        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            var categories = Enumerable.Range(1, 40)
                .Select(index => $"Danh mục cầu lông {index:00}")
                .ToArray();

            var data = categories.Select((name, index) => new Category
            {
                Id = index + 1,
                Name = name,
                Description = $"Danh mục {name}",
                IsActive = true,
                CreatedAt = _seedDateTime.AddDays(index),
                UpdatedAt = _seedDateTime.AddDays(index)
            }).ToList();

            modelBuilder.Entity<Category>().HasData(data);
        }

        /// <summary>
        /// Seed default brands
        /// </summary>
        private static void SeedBrands(ModelBuilder modelBuilder)
        {
            var brands = Enumerable.Range(1, 40)
                .Select(index => $"Thương hiệu {index:00}")
                .ToArray();

            var data = brands.Select((name, index) => new Brand
            {
                Id = index + 1,
                Name = name,
                Description = $"Thương hiệu {name} cho thể thao cầu lông",
                IsActive = true,
                CreatedAt = _seedDateTime.AddDays(index),
                UpdatedAt = _seedDateTime.AddDays(index)
            }).ToList();

            modelBuilder.Entity<Brand>().HasData(data);
        }

        /// <summary>
        /// Seed sample products
        /// </summary>
        private static void SeedProducts(ModelBuilder modelBuilder)
        {
            var productNames = new[]
            {
                "Vợt Yonex Astrox 99",
                "Vợt Yonex Nanoflare 800",
                "Vợt Victor Brave Sword 12",
                "Vợt Li-Ning Turbo 900",
                "Giày Yonex Power Cushion",
                "Giày Victor A830",
                "Giày Li-Ning Sprint",
                "Giày Mizuno Wave Rider",
                "Áo Yonex Training",
                "Áo Victor Sport",
                "Áo Li-Ning Pro",
                "Áo Mizuno Active",
                "Túi đựng vợt Yonex",
                "Túi đựng vợt Victor",
                "Lưới cầu lông Li-Ning",
                "Bóng cầu lông Yonex",
                "Băng quấn cổ tay",
                "Dây căng vợt 3U",
                "Dây căng vợt 4U",
                "Balo thể thao Pro",
                "Cầu lông chuyên nghiệp",
                "Vợt Head Graphene 360",
                "Vợt Babolat X100",
                "Vợt Apacs Z-Force",
                "Giày Head Sprint",
                "Giày Adidas Court",
                "Giày Nike Zoom",
                "Áo Nike Court",
                "Áo Puma Energy",
                "Túi vợt Head",
                "Lưới đánh bóng",
                "Bóng cầu lông premium",
                "Dụng cụ bảo vệ vợt",
                "Bọc cán vợt",
                "Vợt đi kèm dây căng",
                "Vợt cho người mới",
                "Vợt cho người chuyên nghiệp",
                "Giày đệm chống chấn",
                "Áo khoác thời trang",
                "Áo chạy bộ thể thao",
                "Vợt thể thao đa năng",
                "Giày chạy bộ cầu lông",
                "Balo di chuyển thể thao",
                "Bọc bảo vệ vợt",
                "Lưới thi đấu tiêu chuẩn",
                "Bóng chất lượng cao",
                "Vợt niềm tin",
                "Vợt kết hợp công nghệ",
                "Giày nhẹ bám sân",
                "Áo chống nắng",
                "Băng quấn cổ tay chuyên dụng",
                "Dây căng vợt tối ưu",
                "Túi chứa phụ kiện",
                "Vợt săn điểm cao",
                "Vợt đấm mạnh",
                "Vợt khóa gió",
                "Giày cải thiện phản xạ",
                "Áo thể thao mùa hè",
                "Balo cho vận động viên"
            };

            var products = new List<Product>();
            for (var i = 1; i <= 60; i++)
            {
                var productName = productNames[(i - 1) % productNames.Length];
                var categoryId = ((i - 1) % 12) + 1;
                var brandId = ((i - 1) % 12) + 1;

                products.Add(new Product
                {
                    Id = i,
                    Name = $"{productName} {i}",
                    Description = $"Sản phẩm {i} dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.",
                    CategoryId = categoryId,
                    BrandId = brandId,
                    IsActive = true,
                    CreatedAt = _seedDateTime.AddDays(i),
                    UpdatedAt = _seedDateTime.AddDays(i)
                });
            }

            modelBuilder.Entity<Product>().HasData(products);
        }

        /// <summary>
        /// Seed sample product variants
        /// </summary>
        private static void SeedProductVariants(ModelBuilder modelBuilder)
        {
            var variants = new List<ProductVariant>();
            var variantId = 1;

            for (var productId = 1; productId <= 60; productId++)
            {
                var basePrice = 250000 + (productId * 135000) % 5000000;

                for (var index = 1; index <= 2; index++)
                {
                    variants.Add(new ProductVariant
                    {
                        Id = variantId++,
                        ProductId = productId,
                        SKU = $"POP{productId:000}-{index}",
                        VariantLabel = index == 1 ? "Màu Đen" : "Màu Trắng",
                        Price = basePrice + (index * 150000),
                        StockQuantity = 15 + ((productId + index) % 30),
                        IsActive = true
                    });
                }
            }

            modelBuilder.Entity<ProductVariant>().HasData(variants);
        }

        /// <summary>
        /// Seed product images
        /// </summary>
        private static void SeedProductImages(ModelBuilder modelBuilder)
        {
            var images = new List<ProductImage>();
            var imageId = 1;

            for (var productId = 1; productId <= 60; productId++)
            {
                var primaryImage = ((productId - 1) % 8) + 1;

                for (var index = 1; index <= 2; index++)
                {
                    images.Add(new ProductImage
                    {
                        Id = imageId++,
                        ProductId = productId,
                        ImageUrl = $"/img/product/p{((productId + index - 2) % 8) + 1}.jpg",
                        IsPrimary = index == 1,
                        SortOrder = index
                    });
                }
            }

            modelBuilder.Entity<ProductImage>().HasData(images);
        }


        /// <summary>
        /// Seed admin user
        /// </summary>
        private static void SeedAdmin(ModelBuilder modelBuilder)
        {
            var adminUser = new User
            {
                Id = "admin-001",
                UserName = "admin@poppop.com",
                NormalizedUserName = "ADMIN@POPPOP.COM",
                Email = "admin@poppop.com",
                NormalizedEmail = "ADMIN@POPPOP.COM",
                FullName = "Admin PôpPôp",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            // Hash password: "Admin@123"
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<User>().HasData(adminUser);

            // Seed admin role assignment
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = "1"
                }
            );
        }
    }
}
