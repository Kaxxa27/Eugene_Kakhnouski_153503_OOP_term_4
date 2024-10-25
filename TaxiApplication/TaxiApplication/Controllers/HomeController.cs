using Microsoft.AspNetCore.Mvc;

namespace TaxiApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

    }
}
