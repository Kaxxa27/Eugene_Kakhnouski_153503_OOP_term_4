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

		builder.Services.AddControllersWithViews();

		builder.Services.AddScoped<IRepository<User>, EfRepository<User>>();
		builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IBaseResponse<User>, BaseResponse<User>>();

		var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
		builder.Services.AddDbContext<TaxiApplicationDbContext>(options =>
		{
			options.UseSqlite(connection);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        });

		var app = builder.Build();

		app.MapControllerRoute(
		 name: "User",
		 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		 );


		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}"
			);

		app.Run();
	}
}