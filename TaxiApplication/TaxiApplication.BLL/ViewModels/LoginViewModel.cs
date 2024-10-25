using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.BLL.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите логин.")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Укажите пароль.")]
    public string Password { get; set; } = null!;
}
