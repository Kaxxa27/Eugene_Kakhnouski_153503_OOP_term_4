using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Domain.Enum;

namespace TaxiApplication.Domain.Entity;

public class User : Entity
{
	public Role Role { get; set; }
	public string Login { get; set; } = null!;
	public string Password { get; set; } = null!;

	//[NotMapped]
	//[Compare("Password")]
	//public string PasswordConfirm { get; set; } = null!;
}
