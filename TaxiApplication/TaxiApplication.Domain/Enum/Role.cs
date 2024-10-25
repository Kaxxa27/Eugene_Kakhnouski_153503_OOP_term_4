using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaxiApplication.Domain.Enum;

public enum Role
{
	[Display(Name = "Пользователь")]
	Client = 0,
	[Display(Name = "Водитель")]
	Driver = 1,
	[Display(Name = "Админ")]
	Admin = 2,
}
