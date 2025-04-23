using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionID { get; set; }

        public int PermissionID { get; set; }
        public int RoleID { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("RoleID")]
        public Role Role { get; set; } = new Role();

        [ForeignKey("PermissionID")]
        public Permission Permission { get; set; } = new Permission();
    }
}
