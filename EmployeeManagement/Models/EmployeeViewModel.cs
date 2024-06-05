using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
	public class EmployeeViewModel
	{
		public int ID { get; set; }

		[Required]
		public string FirstName { get; set; }

		public string? LastName { get; set; }

		[Required]
		public string EmployeeCode { get; set; }

		[Required]
		public double DateOfJoining { get; set; }  // UTC epoch timestamp

		[Required]
		public DateTime DateOfBirth { get; set; }  // Date in YYYY-MM-DD format

		[Required]
		public int Salary { get; set; }  // Employee salary per month
	}
}
