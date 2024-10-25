using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace TaxiApplication;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		// MVC support 
		builder.Services.AddControllersWithViews();

		// Swagger Support
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Taxi Application API", Version = "v1" });
		});

		
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
		builder.Services.AddScoped<IRepository<TaxiOrder>, EfRepository<TaxiOrder>>();
		builder.Services.AddScoped<IRepository<TaxiApplication.Domain.Entity.Route.Route>, EfRepository<TaxiApplication.Domain.Entity.Route.Route>>();
		builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

		// Services registration. 
		builder.Services.AddScoped<IClientService, ClientService>();
		builder.Services.AddScoped<IAccountService, AccountService>();
		builder.Services.AddScoped<IMapService, MapService>();
		builder.Services.AddScoped<ITaxiOrderService, TaxiOrderService>();
		builder.Services.AddScoped<IBaseResponse<User>, BaseResponse<User>>();
		
		// Database configuration.
		var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
		builder.Services.AddDbContext<TaxiApplicationDbContext>(options =>
		{
			options.UseSqlite(connection);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        });

		var app = builder.Build();

		// Swagger
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => 
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taxi Application API V1");
				c.RoutePrefix = "swagger";
			});
		}
		
		// Authorization and Authorization.
		app.UseAuthentication();
		app.UseAuthorization();

		// For wwwroot
		app.UseStaticFiles();


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