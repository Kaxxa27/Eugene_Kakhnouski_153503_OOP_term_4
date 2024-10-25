using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Domain.Entity.Profile;

public class ClientProfile : UserProfile
{
	public int ClientId { get; set; }
}
