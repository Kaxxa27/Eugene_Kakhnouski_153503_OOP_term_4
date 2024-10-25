using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaxiApplication.BLL.Implementations;
using TaxiApplication.BLL.Interfaces;
using TaxiApplication.DAL;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.DAL.Repositories;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Responce;
using TaxiApplication.Domain.Responce.Interfaces;

namespace TaxiApplication;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		// MVC support 
		builder.Services.AddControllersWithViews();

		// Authentication and authorization.
		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = new PathString("/Account/Login");
				options.AccessDeniedPath = new PathString("/Account/AccessDenied");
			});
		builder.Services.AddAuthorization();

		// Repositories registration.
		builder.Services.AddScoped<IRepository<User>, EfRepository<User>>();
		builder.Services.AddScoped<IRepository<Client>, EfRepository<Client>>();
		builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

		// Services registration. 
		builder.Services.AddScoped<IClientService, ClientService>();
		builder.Services.AddScoped<IAccountService, AccountService>();
		builder.Services.AddScoped<IBaseResponse<User>, BaseResponse<User>>();
		
		// Database configuration.
		var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
		builder.Services.AddDbContext<TaxiApplicationDbContext>(options =>
		{
			options.UseSqlite(connection);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        });

		var app = builder.Build();

		// Authorization and Authorization.
		app.UseAuthentication();
		app.UseAuthorization();


		// Mapping routes to controllers.
		app.MapControllerRoute(
		 name: "area",
		 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		 );


		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}"
			);

		app.Run();
	}
}