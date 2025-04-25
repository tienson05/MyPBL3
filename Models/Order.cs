using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User Users { get; set; } 

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }

}
