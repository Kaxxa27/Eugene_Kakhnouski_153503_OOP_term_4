

namespace TaxiApp.Domain.Entities.PersonEntities;

public abstract class Person : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Country { get; set; }
    public string? Photo { get; set; }
    public int? Age { get; set; }
}
