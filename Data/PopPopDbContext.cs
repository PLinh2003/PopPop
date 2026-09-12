using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PopPop.Data.Entities;
using PopPop.Models;

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
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Vợt cầu lông",
                    Description = "Các loại vợt cầu lông",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Category
                {
                    Id = 2,
                    Name = "Giày cầu lông",
                    Description = "Giày chơi cầu lông",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Category
                {
                    Id = 3,
                    Name = "Quần áo cầu lông",
                    Description = "Trang phục chơi cầu lông",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Category
                {
                    Id = 4,
                    Name = "Phụ kiện cầu lông",
                    Description = "Các phụ kiện liên quan đến cầu lông",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Category
                {
                    Id = 5,
                    Name = "Cầu cầu lông",
                    Description = "Cầu và các vật liệu liên quan",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }

        /// <summary>
        /// Seed default brands
        /// </summary>
        private static void SeedBrands(ModelBuilder modelBuilder)
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name = "Yonex",
                    Description = "Thương hiệu cầu lông hàng đầu từ Nhật Bản",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Brand
                {
                    Id = 2,
                    Name = "Victor",
                    Description = "Thương hiệu cầu lông từ Đài Loan",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Brand
                {
                    Id = 3,
                    Name = "Li-Ning",
                    Description = "Thương hiệu cầu lông từ Trung Quốc",
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                }
            };

            modelBuilder.Entity<Brand>().HasData(brands);
        }

        /// <summary>
        /// Seed sample products
        /// </summary>
        private static void SeedProducts(ModelBuilder modelBuilder)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Vợt Yonex Astrox",
                    Description = "Vợt cầu lông cao cấp",
                    CategoryId = 1,
                    BrandId = 1,
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Product
                {
                    Id = 2,
                    Name = "Giày Victor A830",
                    Description = "Giày cầu lông bền bỉ",
                    CategoryId = 2,
                    BrandId = 2,
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                },
                new Product
                {
                    Id = 3,
                    Name = "Áo Li-Ning",
                    Description = "Áo thể thao cầu lông",
                    CategoryId = 3,
                    BrandId = 3,
                    IsActive = true,
                    CreatedAt = _seedDateTime,
                    UpdatedAt = _seedDateTime
                }
            };

            modelBuilder.Entity<Product>().HasData(products);
        }

        /// <summary>
        /// Seed sample product variants
        /// </summary>
        private static void SeedProductVariants(ModelBuilder modelBuilder)
        {
            var variants = new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = 1,
                    ProductId = 1,
                    SKU = "YONEX-ASTROX-001",
                    Price = 2500000,
                    StockQuantity = 10,
                    IsActive = true
                },
                new ProductVariant
                {
                    Id = 2,
                    ProductId = 2,
                    SKU = "VICTOR-A830-001",
                    Price = 1200000,
                    StockQuantity = 15,
                    IsActive = true
                },
                new ProductVariant
                {
                    Id = 3,
                    ProductId = 3,
                    SKU = "LINING-AO-001",
                    Price = 350000,
                    StockQuantity = 20,
                    IsActive = true
                }
            };

            modelBuilder.Entity<ProductVariant>().HasData(variants);
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
