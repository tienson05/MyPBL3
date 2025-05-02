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
        }

    }
}
