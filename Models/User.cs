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
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty ;
        
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
        
        public int TotalPosts { get; set; } = 0;
        
        public int TotalPurchases { get; set; } = 0;
        
        public double Balance { get; set; } = 0;
        public string Status {  get; set; } = "Active";
        
        public bool IsVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string AvatarUrl { get; set; } = string.Empty;

        public int RoleID { get; set; } = (int)UserRole.Buyer;

        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        // Navigation properties
        public ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public ICollection<Order> BuyerOrders { get; set; } = new HashSet<Order>();
        public ICollection<Order> VendorOrders { get; set; } = new HashSet<Order>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ICollection<UserRating> UserRatings { get; set; } = new HashSet<UserRating>(); //xem lai cach design ERD
        public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
        //trong ERD thieu CreateAt

        [NotMapped]
        public List<int> Permissions { get; set; } = new List<int>();
    }
}
