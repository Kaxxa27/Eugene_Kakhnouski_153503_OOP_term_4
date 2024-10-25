using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TaxiApplication.BLL.Interfaces;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Responce;

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
        public IActionResult Index()
        {
            return View(_userService.GetAllUsers().Result.Data);
        }

        [HttpGet]
        public IActionResult AddUser() { return View(); }

        [HttpPost]
        public async Task<IActionResult> AddUser(TaxiApplication.Domain.Entity.User user)
        {
            if(ModelState.IsValid)
            {
                var response = await _userService.AddUser(user);

                if(response.StatusCode == Domain.Enum.StatusCode.OK) 
                { 
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", response.Description);
			}
			return View(user);
        }
    }
}
