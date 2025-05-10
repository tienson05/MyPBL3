using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AgainPBL3.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }

        [Required]
        public int OrderID { get; set; } 
        [ForeignKey("OrderID")]
        [JsonIgnore]
        public Order Order { get; set; } 

        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [JsonIgnore]
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity {  get; set; }
    }

}
