using EmployeeManagement.Core.Context;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Core.InfrastructureInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly EmployeeContext _context;

		public EmployeeRepository(EmployeeContext context)
		{
			_context = context;
		}

		public async Task<List<Employee>> GetAllEmployess()
		{
			try
			{
                return await _context.Employee.ToListAsync();
            }
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<Employee?> GetEmployeeById(int emoployeeID)
		{
			try
			{
                return await _context.Employee.Where(x => x.ID == emoployeeID).FirstOrDefaultAsync();
            }
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<int> UpdateEmployee(Employee employee)
		{
			try
			{
                _context.Employee.Update(employee);
                return await _context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				return 0;
			}
		}

		public async Task<int> AddEmployee(Employee employee)
		{
			try
			{
                await _context.Employee.AddAsync(employee);
                return await _context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				return 0;
			}
		}

		public async Task<int> DeleteEmployee(Employee employee)
		{
			try
			{
                _context.Employee.Remove(employee);
                return await _context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				return 0;
			}
		}

		public async Task<Employee?> GetEmployeeByEmployeeCode(string employeeCode)
		{
			try
			{
				return await _context.Employee.Where(x => x.EmployeeCode == employeeCode).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				return null;
			}
		}


	}
}
