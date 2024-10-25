namespace TaxiApp.Domain.Entities.OrderEntity;
public class Order : Entity
{
    public double? FullPrice { get; set; }
    public string? PaymentType { get; set; }
    public double? Distance { get; set; }
    public string? UserName { get; set; }
    public string? DriverName { get; set; }
    public string? CarType { get; set; }
    public string? TariffType { get; set; }
    public string? StartAddress { get; set; }
    public string? EndAddress { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}
