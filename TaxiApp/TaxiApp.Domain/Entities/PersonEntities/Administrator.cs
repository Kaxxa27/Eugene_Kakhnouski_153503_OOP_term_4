

namespace TaxiApp.Domain.Entities.PersonEntities;
public class Administrator : Person
{
    public List<Tariff>? TariffList { get; set; }
    public List<Driver>? DriverList { get; set; }
    public List<User>? UserList { get; set; }
    public List<Order>? OrderList { get; set; }
}
