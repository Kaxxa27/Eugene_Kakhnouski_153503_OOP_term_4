namespace TaxiApplication.DAL;

public class TaxiApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<ClientProfile> clientProfiles { get; set; }
	//public DbSet<UserProfile> userProfiles { get; set; }
	public TaxiApplicationDbContext(DbContextOptions<TaxiApplicationDbContext> dbContextOptions) : base(dbContextOptions)
	{
		//Database.EnsureDeleted();
		Database.EnsureCreated();
	}



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<User>()
		//.HasIndex(u => u.Login)
		//.IsUnique();

		User Admin = new User
		{
			Id = 1,
			//Profile = new ClientProfile 
			//{
			//    ClientId = 1,
			//	Name = "Евгений",
			//	Surname = "Кахновский",
			//	Email = "Kaxxa2927@mail.ru",
			//	PhoneNumber = "+375297778899"
			//},	
			Login = "Kaxxa",
			Password = "12345",
			Role = Role.Admin
		};



		modelBuilder.Entity<User>().HasData(Admin);
	
	}

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//    optionsBuilder.UseSqlite("Data Source=TaxiApplicationDataBase.db");
	//}
}
