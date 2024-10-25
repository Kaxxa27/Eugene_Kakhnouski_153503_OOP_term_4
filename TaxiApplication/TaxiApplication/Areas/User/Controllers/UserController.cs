using Microsoft.AspNetCore.Mvc;
using TaxiApplication.BLL.Interfaces;

namespace TaxiApplication.WEB.Areas.User.Controllers
{
	[Area("User")]
	public class UserController : Controller
	{
		IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

		[HttpGet]
        public IActionResult GetAllUsers()
		{
			return View(_userService.GetAllUsers().Result.Data);
		}
	}
}
