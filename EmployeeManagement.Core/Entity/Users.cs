using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entity
{
    [Table("tblUsers")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string UserName { get; set; }

        [ForeignKey("UserRoles")]
        public int Role { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Password { get; set; }

        public virtual UserRoles UserRoles { get; set; }

    }
}
