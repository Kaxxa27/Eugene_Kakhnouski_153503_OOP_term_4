namespace TaxiApplication.WEB.Areas.User.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IClientService _clientService;

        public AdminController(IClientService clientService)
        {
			_clientService = clientService;
        }

        #region HttpGet
        [HttpGet]
        public IActionResult Index()
        {
            return View(_clientService.GetAllClients().Result.Data);
        }

        [HttpGet]
        public IActionResult AddClient() { return View(); }

        [HttpGet]
        public IActionResult UpdateClient(int id) 
        { 
            return View(_clientService.FirstOrDefault(x => x.Id == id).Result.Data); 
        }

        [HttpGet]
        public IActionResult DeleteClient() { return View(); }  
        
        [HttpGet]
        public IActionResult AdminPanel() { return View(_clientService.GetAllClients().Result.Data); }

        [HttpGet]
        public IActionResult FindClient() { return View(); }
        #endregion

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> AddClient(TaxiApplication.Domain.Entity.Client client)
        {
            if (ModelState.IsValid)
            {
                var response = await _clientService.AddClient(client);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", response.Description);
            }
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var response = await _clientService.DeleteClient(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(TaxiApplication.Domain.Entity.Client client)
        {
            var response = await _clientService.UpdateClient(client);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("AdminPanel");
            }
            return View();
        }
        #endregion
    }
}
