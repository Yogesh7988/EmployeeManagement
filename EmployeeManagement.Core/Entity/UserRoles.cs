using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entity
{
    [Table("tblUserRoles")]
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string RoleName { get; set; }

        // Navigation property for users
        public virtual ICollection<Users> Users { get; set; }
    }
}
