using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }

        public int RatingValue { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // lấy đánh giá của nhiều người sau đó chia trung bình lấy ra ranting -> redesign ERD
        public ICollection<UserRating> UserRatings { get; set; } = new List<UserRating>();
    }

}
