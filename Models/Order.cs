using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AgainPBL3.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        [JsonIgnore]
        public User Buyer { get; set; }

        [Required]
        public int VendorId { get; set; }

        [ForeignKey("VendorId")]
        [JsonIgnore]
        public User Vendor { get; set; }

        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CancelReason { get; set; } =string.Empty;
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PayMethod { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }

}
