using EmployeeManagement.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.ApplicationInterface
{
	public interface IEmployeeService
	{
		Task<List<Employee>> GetAllEmployess();
		Task<bool> UpdateEmployee(Employee employee);
		Task<bool> AddEmployee(Employee employee);
		Task<bool> DeleteEmployee(int employeeId);
		Task<Employee> GetEmployeeById(int employeeId);

	}
}
