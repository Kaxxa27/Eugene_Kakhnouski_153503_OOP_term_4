using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Domain.Enum;

namespace TaxiApplication.Domain.Entity;

public class User : Entity
{
	[Required(ErrorMessage = "Введите роль пользователя.")]
    public Role Role { get; set; }

	[Required(ErrorMessage = "Укажите имя пользователя.")]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов.")]
	public string Name { get; set; } = string.Empty;
	
	[Required(ErrorMessage = "Укажите фамилию пользователя.")]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов.")]
	public string Surname { get; set; } = string.Empty;

	[Required(ErrorMessage = "Укажите пароль.")]
	public string Password { get; set; } = string.Empty;

	[EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
	[Required(ErrorMessage = "Укажите вашу электронную почту.")]
	public string Email { get; set; } = string.Empty;

	[Phone(ErrorMessage = "Некорректный номер телефона.")]
	[Required(ErrorMessage = "Укажите ваш номер телефона.")]
	public string PhoneNumber { get; set; } = string.Empty;
}
