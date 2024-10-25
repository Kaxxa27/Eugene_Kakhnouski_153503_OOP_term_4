namespace TaxiApp.Domain.Entities.TaxiCompanyEntity;
public class TaxiCompany : Entity
{
    public List<Tariff>? TariffList { get; set; }
    public List<Driver>? DriverList { get; set; }
    public List<Administrator>? AdministratorList { get; set; }
    public List<User>? UserList { get; set; }
    public List<Order>? OrderList { get; set; }
    public List<Car>? CarList { get; set; }
}
