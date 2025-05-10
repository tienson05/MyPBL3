using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgainPBL3.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; } // Khóa ngoại đến User
        [ForeignKey("UserID")]
        public User User { get; set; } // Navigation property
        public List<CartItem> CartItems { get; set; } = new List<CartItem>(); // Danh sách sản phẩm trong giỏ
    }
}
