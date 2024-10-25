namespace TaxiApp.Persistence.Data;

public class TaxiAppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Administrator> Administrators => Set<Administrator>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Tariff> Tariffs => Set<Tariff>();
    public DbSet<TaxiCompany> TaxiCompanies => Set<TaxiCompany>();
    public TaxiAppDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
}


