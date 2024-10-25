using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Enum;

namespace TaxiApplication.DAL;

public class TaxiApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public TaxiApplicationDbContext(DbContextOptions<TaxiApplicationDbContext> dbContextOptions) : base(dbContextOptions)
	{
		//Database.EnsureDeleted();
		Database.EnsureCreated();
	}



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		
		User Admin = new User
		{
			Id = 1,
			Name = "Евгений",
			Surname = "Кахновский",
			Email = "Kaxxa2927@mail.ru",
			Password = "12345",
			PhoneNumber = "+375297778899",
			Role = Role.Admin
		};



		modelBuilder.Entity<User>().HasData(Admin);
	
	}

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//    optionsBuilder.UseSqlite("Data Source=TaxiApplicationDataBase.db");
	//}
}
