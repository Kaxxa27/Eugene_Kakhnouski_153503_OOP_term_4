namespace TaxiApp.Domain.Entities.CarEntity;

public class Car : Entity
{
    public string? Number { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public Color? Color { get; set; }
    public bool? IsBusy { get; set; }
}
