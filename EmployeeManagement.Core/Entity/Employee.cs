using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core.Entity
{
    [Table("tblEmployees")]
    public class Employee
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
        [Column(TypeName = "nvarchar(255)")]
        public string EmployeeCode { get; set; }

        [Required]
        public double DateOfJoining { get; set; }  // UTC epoch timestamp

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }  // Date in YYYY-MM-DD format

        [Required]
        public int Salary { get; set; }  // Employee salary per month
    }
}
