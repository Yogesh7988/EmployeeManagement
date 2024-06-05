using AutoMapper;
using EmployeeManagement.Core.ApplicationInterface;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserServices userServices, IMapper mapper)
        {
            _logger = logger;
			_userServices = userServices;
            _mapper = mapper;
		}

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<Users>(loginViewModel);
                var isSucess = await _userServices.IsAuthenticated(user);
				if (isSucess)
                {
					var token = await _userServices.GenerateJwtToken(user);
                    if(token != null)
                    {
                        SetToken(token);
						return RedirectToAction("Index", "Employee");
					}
					
				}
            }
            TempData["ErrorMessage"] = "Invalid username or password";

            return View("Index", loginViewModel);
		}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            switch (code)
            {
                case 401:
                    TempData["ErrorMessage"] = "Unauthorized Access";
                    break;
                case 404:
                    TempData["ErrorMessage"] = "Sorry, the resource you requested could not be found.";
                    break;
                case 500:
                    TempData["ErrorMessage"] = "Sorry, something went wrong on the server.";
                    break;
                default:
                    TempData["ErrorMessage"] = "An unexpected error occurred.";
                    break;
            }
            return RedirectToAction(nameof(Index));
        }

        
        private void SetToken(string token)
        {
			var cookieOptions = new CookieOptions
			{
				Expires = DateTime.UtcNow.AddDays(7), // Set cookie expiry
				HttpOnly = false, // Ensure the cookie is only accessible via HTTP (not JavaScript)
				Secure = true, // Ensure the cookie is only sent over HTTPS
				SameSite = SameSiteMode.Strict // Protect against CSRF attacks
			};
			Response.Cookies.Append("accessToken", token, cookieOptions);

		}
    }
}
