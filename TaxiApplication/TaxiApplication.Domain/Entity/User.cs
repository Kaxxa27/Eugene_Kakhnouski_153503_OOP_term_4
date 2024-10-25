using System;
using System.Collections.Generic;
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
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
