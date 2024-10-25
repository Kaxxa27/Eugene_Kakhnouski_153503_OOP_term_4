using System;

namespace TaxiApp.Persistence.Repository;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly TaxiAppDbContext _context;

    private readonly Lazy<IRepository<User>> _userRepository;
    private readonly Lazy<IRepository<Driver>> _driverRepository;
    private readonly Lazy<IRepository<Administrator>> _administratorRepository;
    private readonly Lazy<IRepository<Car>> _carRepository;
    private readonly Lazy<IRepository<Order>> _orderRepository;
    private readonly Lazy<IRepository<Tariff>> _tariffRepository;
    private readonly Lazy<IRepository<TaxiCompany>> _taxicompanyRepository;
    public EfUnitOfWork(TaxiAppDbContext context)
    {
        _context = context;

        #region InitializationRepo

        _userRepository = new Lazy<IRepository<User>>(() =>
       new EfRepository<User>(context));

        _driverRepository = new Lazy<IRepository<Driver>>(() =>
        new EfRepository<Driver>(context));

        _administratorRepository = new Lazy<IRepository<Administrator>>(() =>
        new EfRepository<Administrator>(context));

        _carRepository = new Lazy<IRepository<Car>>(() =>
        new EfRepository<Car>(context));

        _orderRepository = new Lazy<IRepository<Order>>(() =>
        new EfRepository<Order>(context));

        _tariffRepository = new Lazy<IRepository<Tariff>>(() =>
        new EfRepository<Tariff>(context));

        _taxicompanyRepository = new Lazy<IRepository<TaxiCompany>>(() =>
        new EfRepository<TaxiCompany>(context));
        #endregion
    }

    #region PropetiesRepo
    public IRepository<User> UserRepository => _userRepository.Value;

    public IRepository<Driver> DriverRepository => _driverRepository.Value;

    public IRepository<Administrator> AdministratorRepository => _administratorRepository.Value;

    public IRepository<Car> CarRepository => _carRepository.Value;

    public IRepository<Order> OrderRepository => _orderRepository.Value;

    public IRepository<Tariff> TariffRepository => _tariffRepository.Value;

    public IRepository<TaxiCompany> TaxiCompanyRepository => _taxicompanyRepository.Value;
    #endregion

    public async Task CreateDatabaseAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }
    public async Task RemoveDatbaseAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }
    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }

}
