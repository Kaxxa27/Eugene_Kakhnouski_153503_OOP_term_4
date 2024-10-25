using Microsoft.EntityFrameworkCore;
using TaxiApplication.DAL;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.DAL.Repositories;
using TaxiApplication.Domain.Entity;

namespace TaxiApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IRepository<User>, EfRepository<User>>();
        builder.Services.AddScoped<IUnitOfWork,  EfUnitOfWork>();

        var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
        builder.Services.AddDbContext<TaxiApplicationDbContext>(options =>
            options.UseSqlite(connection));

        var app = builder.Build();



        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );

        app.Run();
    }
}