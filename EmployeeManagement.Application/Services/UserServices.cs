using EmployeeManagement.Core.ApplicationInterface;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Core.InfrastructureInterface;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly PasswordHasher _passwordHasher;

		public UserServices(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_passwordHasher = new PasswordHasher();
		}

		public async Task<string?> GenerateJwtToken(Users user)
		{
			var userDetails = await _userRepository.GetUserDetailsByUserName(user);
			if (userDetails != null)
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var tokenKey = Encoding.ASCII.GetBytes("u7wHbRzK7q6tJw3uL5zC6P7K3tG6u7wzK7q9rBxYzE="); // Replace with your secret key
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new[]
					{
					new Claim("id", userDetails.ID.ToString()),
					new Claim("firstName", userDetails.FirstName.ToString()),
					new Claim(ClaimTypes.Role, userDetails.UserRoles.RoleName)
				}),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
			}
			return null;
		}

		public async Task<bool> IsAuthenticated(Users user)
		{
			var userDetails = await _userRepository.GetUserDetailsByUserName(user);
			if(userDetails != null)
			{
				var hashPassword = PaswwordEncription(user.Password);
				bool isCoorectPassword = VerifyPassword(hashPassword, user.Password);
				return isCoorectPassword;
			}
			return false;
		}
		
		private string PaswwordEncription(string password)
		{
			return _passwordHasher.HashPassword(password);
		}

		private bool VerifyPassword(string hashedPassword, string providedPassword)
		{
			var result = _passwordHasher.VerifyHashedPassword(hashedPassword, providedPassword);
			return result == PasswordVerificationResult.Success;
		}

	}
}
