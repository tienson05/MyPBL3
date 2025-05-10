using System.Security.Cryptography;
using AgainPBL3.Models;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(oi => oi.OrderID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Order thì xóa OrderItem

            modelBuilder.Entity<OrderDetail>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductID)
                .OnDelete(DeleteBehavior.Restrict); // Không cho xóa Product nếu còn OrderItem


            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserID)
                .OnDelete(DeleteBehavior.Restrict);  // Tắt cascade delete

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRating>()
                .HasOne(ur => ur.SenderUser)
                .WithMany()
                .HasForeignKey(w => w.SenderUserID)
                .OnDelete(DeleteBehavior.Restrict);  // Tắt cascade delete
                                                     // Cấu hình ánh xạ tên cột (đã có từ trước)
            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasColumnName("CreatedAt");

            modelBuilder.Entity<Order>()
                .Property(o => o.CompletedAt)
                .HasColumnName("CompletedAt");

            // Cấu hình quan hệ giữa Order và User cho Buyer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(u => u.BuyerOrders)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh cascade delete gây xung đột

            // Cấu hình quan hệ giữa Order và User cho Vendor
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Vendor)
                .WithMany(u => u.VendorOrders)
                .HasForeignKey(o => o.VendorId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh cascade delete gây xung đột

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ Cart-CartItem
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ CartItem-Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductID) // Sửa ProductID thành ProductId
                .OnDelete(DeleteBehavior.NoAction);


            // Seeding dữ liệu mẫu
    //        modelBuilder.Entity<Role>().HasData(
    //            new Role { RoleID = 1, RoleName = "Admin", Description = "Administrator with full access" },
    //            new Role { RoleID = 2, RoleName = "User", Description = "Regular user with limited access" }
    //);

            //modelBuilder.Entity<Permission>().HasData(
            //    new Permission { PermissionID = 1, Name = "ManageProducts", Description = "Can create and edit products" },
            //    new Permission { PermissionID = 2, Name = "ViewOrders", Description = "Can view order history" }
            //);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Username = "john_doe",
                    Email = "john@example.com",
                    HashedPassword = "hashed_password_1",
                    Name = "John Doe",
                    Gender = true,
                    BirthOfDate = new DateTime(1990, 1, 1),
                    PhoneNumber = "1234567890",
                    Address = "123 Main St",
                    TotalPosts = 5,
                    TotalPurchases = 2,
                    Balance = 1000.0,
                    Status = "Active",
                    IsVerified = true,
                    LastLoginAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://example.com/avatar1.jpg",
                    RoleID = 1
                },
                new User
                {
                    UserID = 2,
                    Username = "jane_smith",
                    Email = "jane@example.com",
                    HashedPassword = "hashed_password_2",
                    Name = "Jane Smith",
                    Gender = false,
                    BirthOfDate = new DateTime(1995, 5, 15),
                    PhoneNumber = "0987654321",
                    Address = "456 Oak St",
                    TotalPosts = 3,
                    TotalPurchases = 1,
                    Balance = 500.0,
                    Status = "Active",
                    IsVerified = true,
                    LastLoginAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://example.com/avatar2.jpg",
                    RoleID = 2
                }
            );

            //modelBuilder.Entity<RolePermission>().HasData(
            //    new RolePermission { RolePermissionID = 1, PermissionID = 1, RoleID = 1, IsActive = true },
            //    new RolePermission { RolePermissionID = 2, PermissionID = 2, RoleID = 2, IsActive = true }
            //);

            //modelBuilder.Entity<UserPermission>().HasData(
            //    new UserPermission { UserPermissionID = 1, UserID = 1, PermissionID = 1 },
            //    new UserPermission { UserPermissionID = 2, UserID = 2, PermissionID = 2 }
            //);

            //modelBuilder.Entity<Rating>().HasData(
            //    new Rating { RatingID = 1, RatingValue = 5, CreatedAt = DateTime.Now },
            //    new Rating { RatingID = 2, RatingValue = 4, CreatedAt = DateTime.Now }
            //);

            //modelBuilder.Entity<UserRating>().HasData(
            //    new UserRating { UserRatingID = 1, RatingID = 1, SenderUserID = 2, Comment = "Great product!", UserID = 1 },
            //    new UserRating { UserRatingID = 2, RatingID = 2, SenderUserID = 1, Comment = "Good quality", UserID = 2 }
            //);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Electronics",
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Clothing",
                    CreatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    UserID = 1,
                    CategoryID = 1,
                    Title = "Smartphone",
                    Price = 599.99,
                    Description = "Latest model smartphone",
                    Condition = "New",
                    Images = "https://example.com/smartphone.jpg",
                    Location = "New York",
                    Status = "Available",
                    CreatedAt = DateTime.Now
                },
                new Product
                {
                    ProductID = 2,
                    UserID = 2,
                    CategoryID = 2,
                    Title = "T-Shirt",
                    Price = 19.99,
                    Description = "Cotton T-shirt",
                    Condition = "New",
                    Images = "https://example.com/tshirt.jpg",
                    Location = "Los Angeles",
                    Status = "Available",
                    CreatedAt = DateTime.Now
                },
                new Product
                {
                    ProductID = 3,
                    UserID = 1,
                    CategoryID = 1,
                    Title = "Laptop",
                    Price = 999.99,
                    Description = "High-performance laptop",
                    Condition = "New",
                    Images = "https://example.com/laptop.jpg",
                    Location = "New York",
                    Status = "Available",
                    CreatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, UserID = 1 }
            );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, CartId = 1, ProductID = 1, Quantity = 2 },
                new CartItem { Id = 2, CartId = 1, ProductID = 2, Quantity = 1 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderID = 1,
                    BuyerId = 2,
                    VendorId = 1,
                    TotalPrice = 619.97m,
                    Status = "Pending",
                    CancelReason = "",
                    CreatedAt = DateTime.Now,
                    PayMethod = "Chuyển khoản"
                }
            );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailID = 1, OrderID = 1, ProductID = 1, Price = 599.99m, Quantity = 1 },
                new OrderDetail { OrderDetailID = 2, OrderID = 1, ProductID = 2, Price = 19.99m, Quantity = 1 }
            );

            //modelBuilder.Entity<Wishlist>().HasData(
            //    new Wishlist
            //    {
            //        WishlistID = 1,
            //        UserID = 1,
            //        ProductID = 3,
            //        CreatedAt = DateTime.Now
            //    }
            //);
        }

    }
}
