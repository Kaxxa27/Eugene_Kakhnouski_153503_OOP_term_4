using TaxiApplication.Domain.Entity.Profile;

namespace TaxiApplication.Domain.Entity;

public class Client : User
{
    public virtual ClientProfile Profile { get; set; } = null!;
}
