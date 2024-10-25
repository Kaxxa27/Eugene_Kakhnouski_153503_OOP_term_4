using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaxiApplication.Domain.Entity;

namespace TaxiApplication.WEB.Areas.User.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("[area]/[controller]")]
    public class AdminController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ITaxiOrderService _taxiOrderService;

        public AdminController(IClientService clientService, ITaxiOrderService taxiOrderService)
        {
            _clientService = clientService;
            _taxiOrderService = taxiOrderService;
        }

        #region HttpGet

        /// <summary>
        /// Отображает главную страницу администраторов со списком клиентов.
        /// </summary>
        /// <returns>Представление списка клиентов.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
        public IActionResult Index()
        {
            return View(_clientService.GetAllClients().Result.Data);
        }

        /// <summary>
        /// Отображает страницу для добавления нового клиента.
        /// </summary>
        /// <returns>Представление страницы добавления клиента.</returns>
        [HttpGet("add-client")]
        public IActionResult AddClient() 
        {
            return View(); 
        }

        /// <summary>
        /// Отображает страницу для обновления информации о клиенте.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>Представление страницы обновления клиента.</returns>
        [HttpGet("update-client/{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        public IActionResult UpdateClient(int id) 
        { 
            return View(_clientService.FirstOrDefault(x => x.Id == id).Result.Data); 
        }

        /// <summary>
        /// Отображает страницу для удаления клиента.
        /// </summary>
        /// <returns>Представление страницы удаления клиента.</returns>
        [HttpGet("delete-client")]
        [ProducesResponseType(200)]
        public IActionResult DeleteClient() 
        { 
            return View(); 
        }  

        /// <summary>
        /// Отображает панель администратора с списком клиентов.
        /// </summary>
        /// <returns>Представление панели администратора.</returns>
        [HttpGet("admin-panel")]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
        public IActionResult AdminPanel() 
        { 
            return View(_clientService.GetAllClients().Result.Data); 
        }

        /// <summary>
        /// Отображает страницу для поиска клиентов.
        /// </summary>
        /// <returns>Представление страницы поиска клиентов.</returns>
        [HttpGet("find-client")]
        [ProducesResponseType(200)]
        public IActionResult FindClient() 
        { 
            return View(); 
        }

        /// <summary>
        /// Отображает все заказы такси для указанного клиента.
        /// </summary>
        /// <param name="userId">Идентификатор клиента.</param>
        /// <returns>Представление со списком заказов клиента.</returns>
        [HttpGet("get-taxi-orders/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<TaxiOrder>), 200)]
        public IActionResult GetTaxiOrdersForAdmin(int userId)
        {
            return View(_taxiOrderService.GetAllClientTaxiOrders(userId).Result.Data);
        }

        #endregion

        #region HttpPost

        /// <summary>
        /// Добавляет нового клиента.
        /// </summary>
        /// <param name="client">Модель клиента для добавления.</param>
        /// <returns>Перенаправление на главную страницу или отображение ошибки.</returns>
        [HttpPost("add-client")]
        [ProducesResponseType(302)]
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

        /// <summary>
        /// Удаляет клиента по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор клиента для удаления.</param>
        /// <returns>Перенаправление на главную страницу.</returns>
        [HttpPost("delete-client/{id}")]
        [ProducesResponseType(302)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var response = await _clientService.DeleteClient(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        /// <summary>
        /// Удаляет заказ по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа для удаления.</param>
        /// <returns>Перенаправление на главную страницу или на страницу индекса.</returns>
        [HttpPost("delete-order/{id}")]
        [ProducesResponseType(302)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _taxiOrderService.DeleteTaxiOrder(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// Обновляет информацию о клиенте.
        /// </summary>
        /// <param name="client">Модель клиента с обновленными данными.</param>
        /// <returns>Перенаправление на панель администратора или отображение ошибки.</returns>
        [HttpPost("update-client")]
        [ProducesResponseType(302)]
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
