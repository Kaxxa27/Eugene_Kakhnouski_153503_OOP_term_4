namespace TaxiApplication.Domain.Entity;

public class User : Entity
{
	public Role Role { get; set; }
	public string Login { get; set; } = null!;
	public string Password { get; set; } = null!;

	[NotMapped]
	[Compare("Password")]
	public string PasswordConfirm { get; set; } = null!;
}
