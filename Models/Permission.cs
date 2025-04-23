using System.ComponentModel.DataAnnotations;

namespace AgainPBL3.Models
{
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
    }
}
