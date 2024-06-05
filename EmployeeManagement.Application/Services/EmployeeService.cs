using EmployeeManagement.Core.ApplicationInterface;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Core.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<List<Employee>> GetAllEmployess()
		{
			return await _employeeRepository.GetAllEmployess();
		}

		public async Task<bool> UpdateEmployee(Employee employee)
		{
			var employeeDetails = await _employeeRepository.GetEmployeeById(employee.ID);
			if(employeeDetails != null)
			{
				employeeDetails.FirstName = employee.FirstName;
				employeeDetails.LastName = employee.LastName;
				employeeDetails.DateOfBirth = employee.DateOfBirth;
				employeeDetails.Salary = employee.Salary;
				var isUpdated = await _employeeRepository.UpdateEmployee(employeeDetails);
				if(isUpdated > 0)
				{
					return true;
				}
			}
			return false;			
		}

		public async Task<bool> AddEmployee(Employee employee)
		{
			var employeeDetails = await _employeeRepository.GetEmployeeByEmployeeCode(employee.EmployeeCode);
			if (employeeDetails == null)
			{
				employee.DateOfJoining = ConvertToEpoch(DateTime.UtcNow);
                var isAdded = await _employeeRepository.AddEmployee(employee);
				if (isAdded > 0)
				{
					return true;
				}
			}
			return false;
		}

		public async Task<bool> DeleteEmployee(int employeeId)
		{
			var employeeDetails = await _employeeRepository.GetEmployeeById(employeeId);
			if (employeeDetails != null)
			{
				var isDeleted = await _employeeRepository.DeleteEmployee(employeeDetails);
				if (isDeleted > 0)
				{
					return true;
				}
			}
			return false;
		}

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await _employeeRepository.GetEmployeeById(employeeId);
        }

        private static double ConvertToEpoch(DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime);
            return dto.ToUnixTimeSeconds();
        }

        private static DateTime ConvertFromEpoch(double epoch)
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds((long)epoch);
            return dto.UtcDateTime;
        }

    }
}
