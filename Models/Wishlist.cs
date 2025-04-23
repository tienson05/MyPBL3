using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    [Table("Wishlists")]
    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; } = new User();

        public int ProductID { get; set; }
        
        [ForeignKey("ProductID")]
        public Product Product { get; set; } = new Product();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
