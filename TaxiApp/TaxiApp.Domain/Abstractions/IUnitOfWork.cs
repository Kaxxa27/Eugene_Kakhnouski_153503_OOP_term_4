namespace TaxiApp.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Driver> DriverRepository { get; }
    IRepository<Administrator> AdministratorRepository { get; }
    IRepository<Car> CarRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<Tariff> TariffRepository { get; }
    IRepository<TaxiCompany> TaxiCompanyRepository { get; }
    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}