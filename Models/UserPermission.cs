using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserPermissionID { get; set; }

        public int UserID { get; set; }
        public int PermissionID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; } = new User();

        [ForeignKey("PermissionID")]
        public Permission Permission { get; set; } = new Permission();
    }
}
