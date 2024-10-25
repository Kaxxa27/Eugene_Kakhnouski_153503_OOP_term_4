
namespace TaxiApp.Domain.Entities.PersonEntities;

public class Driver : Person
{
    public string? CarType { get; set; }
    public string? TariffType { get; set; }
    public bool? IsBusy { get; set; }
    public double? Rating { get; set; }
}
