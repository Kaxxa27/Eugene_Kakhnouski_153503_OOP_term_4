namespace TaxiApp.Domain.Entities.PersonEntities;
public class User : Person
{
    public string? BankCardNumber { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public double? Rating { get; set; }
}
