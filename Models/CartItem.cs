using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AgainPBL3.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; } // Khóa ngoại đến Cart
        [ForeignKey("CartId")]
        [JsonIgnore]
        public Cart Cart { get; set; } // Navigation property
        [Required]
        public int ProductID { get; set; } // Khóa ngoại đến Product
        [ForeignKey("ProductID")]
        public Product Product { get; set; } // Navigation property
        public int Quantity { get; set; } // Số lượng sản phẩm
    }
}
