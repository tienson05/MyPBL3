using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AgainPBL3.Models
{
    public enum UserRole
    {
        Admin = 1,
        Buyer = 2,
        Seller = 3,
        Manager = 4,
    }

    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        public string HashedPassword { get; set; } = string.Empty ;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public bool Gender { get; set; } 
        
        public DateTime? BirthOfDate { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address {  get; set; } = string.Empty;
        
        public double TotalPosts { get; set; } = 0;
        
        public double TotalPurchases { get; set; } = 0;
        
        public string Status {  get; set; } = "Active";
        
        public bool IsVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [Url]
        public string AvatarUrl { get; set; } = string.Empty;

        public int RoleID { get; set; } = (int)UserRole.Buyer;

        [ForeignKey("RoleID")]
        public Role Role { get; set; } = new Role();

        // Navigation properties
        public ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ICollection<UserRating> UserRatings { get; set; } = new HashSet<UserRating>(); //xem lai cach design ERD
        public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
        //trong ERD thieu CreateAt

    }
}
