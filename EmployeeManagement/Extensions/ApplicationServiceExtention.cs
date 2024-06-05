using EmployeeManagement.Application.Services;
using EmployeeManagement.Core.ApplicationInterface;
using EmployeeManagement.Core.Context;
using EmployeeManagement.Core.InfrastructureInterface;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeManagement.Extensions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserServices, UserServices>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IEmployeeService, EmployeeService>();

			services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
			services.AddDbContext<UserContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("dbUsers")));
            services.AddDbContext<EmployeeContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("dbEmployees")));
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("u7wHbRzK7q6tJw3uL5zC6P7K3tG6u7wzK7q9rBxYzE=")),
                      ValidateIssuer = false,
                      ValidIssuer = "issue",
                      ValidateAudience = false,
                      ValidAudience = "audience",
                      ValidateLifetime = true,
                  };
                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          context.Token = context.Request.Cookies["accessToken"];
                          return Task.CompletedTask;
                      }
                  };
              });
			services.AddAutoMapper(typeof(CustomProfiles));
			services.AddAuthorization();
            return services;
        }
    }
}
