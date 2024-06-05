using EmployeeManagement.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.InfrastructureInterface
{
	public interface IUserRepository
	{
		Task<Users?> GetUserDetailsByUserName(Users users);
	}
}
