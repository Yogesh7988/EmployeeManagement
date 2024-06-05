using EmployeeManagement.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.InfrastructureInterface
{
	public interface IEmployeeRepository
	{
		Task<List<Employee>> GetAllEmployess();
		Task<Employee?> GetEmployeeById(int emoployeeID);
		Task<int> UpdateEmployee(Employee employee);
		Task<int> AddEmployee(Employee employee);
		Task<int> DeleteEmployee(Employee employee);
		Task<Employee?> GetEmployeeByEmployeeCode(string employeeCode);
	}
}
