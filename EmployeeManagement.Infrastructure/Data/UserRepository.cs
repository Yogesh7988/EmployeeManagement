using EmployeeManagement.Core.Context;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Core.InfrastructureInterface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly UserContext _context;

		public UserRepository(UserContext context)
		{
			_context = context;
		}

		public async Task<Users?> GetUserDetailsByUserName(Users users)
		{
			try
			{
				return await _context.Users.Where(us => us.UserName == users.UserName).Include(e => e.UserRoles).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}
}
