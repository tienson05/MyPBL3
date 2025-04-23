using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgainPBL3.Models
{
    public class UserRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRatingID { get; set; }

        [Required]
        public int RatingID { get; set; }
        [Required]
        public int SenderUserID { get; set; }

        public string Comment { get; set; } = string.Empty;

        // Khóa ngoại tới bảng Rating
        [ForeignKey("RatingID")]
        public Rating Rating { get; set; } = new Rating();

        // Khóa ngoại tới bảng User cho người gửi
        [ForeignKey("SenderUserID")]
        public User SenderUser { get; set; } = new User();
    }
}
