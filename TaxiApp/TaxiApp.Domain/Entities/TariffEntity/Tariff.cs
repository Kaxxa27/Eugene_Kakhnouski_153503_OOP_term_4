namespace TaxiApp.Domain.Entities.TariffEntity;
public class Tariff : Entity
{
    public string? Title { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
}
