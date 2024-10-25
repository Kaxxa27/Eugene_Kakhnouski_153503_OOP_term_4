using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Enum;

namespace TaxiApplication.Controllers
{
    public class HomeController : Controller
    {

        IUnitOfWork _repository;
        public HomeController(IUnitOfWork repository)
        {
            _repository = repository; 
        }
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult AddItem() => View();
        [HttpPost]
        public IActionResult AddItem(User user)
        {
            _repository.UserRepository.AddAsync(user);
            return RedirectToAction("Index");
        }
    }
}
