using EmployeeManagement.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.ApplicationInterface
{
	public interface IUserServices
	{
		Task<bool> IsAuthenticated(Users user);
		Task<string?> GenerateJwtToken(Users user);
	}
}
