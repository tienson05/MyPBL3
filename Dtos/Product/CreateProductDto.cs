using AgainPBL3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Dtos.Product
{
    public class CreateProductDto
    {
        public int CategoryID { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
