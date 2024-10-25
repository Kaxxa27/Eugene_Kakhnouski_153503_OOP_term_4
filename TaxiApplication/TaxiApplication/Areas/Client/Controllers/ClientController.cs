namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Client")]
[ApiController]
[Route("api/[area]/[controller]")]
public class ClientController : Controller
{
    private readonly ITaxiOrderService _taxiOrderService;
    private readonly IMapService _mapService;
    private readonly IClientService _clientService;

    public ClientController(ITaxiOrderService taxiOrderService, IMapService mapService, IClientService clientService)
    {
        _taxiOrderService = taxiOrderService;
        _mapService = mapService;
        _clientService = clientService;
    }

    private bool CheckUserStatus()
    {
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var All_Clients = (_clientService.GetAllClients()).Result.Data;
        var client = All_Clients!.FirstOrDefault(c => c.Id == ID);
        return client is null;
    }

    /// <summary>
    /// Отображает страницу редактирования профиля клиента.
    /// </summary>
    /// <returns>Представление страницы редактирования профиля.</returns>
    [HttpGet("profile-edit")]
    [ProducesResponseType(typeof(Client), 200)]
    public async Task<IActionResult> ProfileEdit()
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        var client = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data;
        return View(client);
    }

    /// <summary>
    /// Обновляет информацию о профиле клиента.
    /// </summary>
    /// <param name="client">Модель клиента с обновленными данными.</param>
    /// <returns>Представление с обновленными данными клиента или страницу с ошибкой.</returns>
    [HttpPost("profile-edit")]
    [ProducesResponseType(typeof(Client), 200)]
    public async Task<IActionResult> ProfileEdit(Client client)
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });

        client.Profile.Photo = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data!.Profile.Photo;
        var response = await _clientService.UpdateClient(client);

        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(client);
        }
        return View(client); // Страница ошибки
    }

    /// <summary>
    /// Загружает фото профиля клиента.
    /// </summary>
    /// <param name="photoFile">Файл с изображением профиля.</param>
    /// <returns>Перенаправление на страницу редактирования профиля.</returns>
    [HttpPost("upload-photo")]
    [ProducesResponseType(302)]
    public async Task<IActionResult> UploadPhoto(IFormFile photoFile)
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        try
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                byte[] photoBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await photoFile.CopyToAsync(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
                var client = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data;
                client!.Profile.Photo = photoBytes;
                await _clientService.UpdateClient(client);
            }

            return RedirectToAction("ProfileEdit");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[ClientController.UploadPhoto] error: {ex.Message}");
            return RedirectToAction("ProfileEdit");
        }
    }

    /// <summary>
    /// Удаляет профиль клиента.
    /// </summary>
    /// <param name="client">Модель клиента для удаления.</param>
    /// <returns>Перенаправление на страницу выхода из аккаунта.</returns>
    [HttpPost("delete-profile")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteProfile(Client client)
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        try
        {
            await _clientService.DeleteClient(client.Id);
            return RedirectToAction("Logout", "Account", new { area = "" });
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[ClientController.DeleteProfile] error: {ex.Message}");
            return RedirectToAction("Logout", "Account", new { area = "" });
        }
    }

    /// <summary>
    /// Отображает страницу для создания заказа такси.
    /// </summary>
    /// <returns>Представление страницы создания заказа такси.</returns>
    [HttpGet("create-taxi-order")]
    [ProducesResponseType(302)]
    [ProducesResponseType(200)]
    public IActionResult CreateTaxiOrder()
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        return View();
    }

    /// <summary>
    /// Создает новый заказ такси.
    /// </summary>
    /// <param name="taxiOrderViewModel">Модель заказа такси.</param>
    /// <returns>Перенаправление на страницу карты или обратно на страницу создания заказа в случае ошибки.</returns>
    [HttpPost("create-taxi-order")]
    [ProducesResponseType(302)]
    public async Task<IActionResult> CreateTaxiOrder(TaxiOrderViewModel taxiOrderViewModel)
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        try
        {
            var request = (await _mapService.CreateRouteRequest(taxiOrderViewModel)).Data;
            taxiOrderViewModel = (await _mapService.CollectInfoFromRequest(request, taxiOrderViewModel)).Data!;

            // Проверка на корректность заказа
            if (taxiOrderViewModel.Route.Distance == 0 && taxiOrderViewModel.Route.Time.TotalSeconds == 0)
            {
                return RedirectToAction("CreateTaxiOrder");
            }

            taxiOrderViewModel.Order.Price = (await _taxiOrderService.CalculatePrice(taxiOrderViewModel.Order)).Data;
            var json = JsonSerializer.Serialize(taxiOrderViewModel);

            TempData["taxiOrderViewModel_JSON"] = json;
            return RedirectToAction(actionName: "Index", controllerName: "Map", new { area = "Map" });
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[ClientController.CreateTaxiOrder] error: {ex.Message}");
            return RedirectToAction("CreateTaxiOrder");
        }
    }

    /// <summary>
    /// Отображает все заказы такси клиента.
    /// </summary>
    /// <returns>Представление страницы со списком заказов такси.</returns>
    [HttpGet("get-taxi-orders")]
    [ProducesResponseType(typeof(TaxiOrder), 200)]
    public IActionResult GetTaxiOrders()
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        return View(_taxiOrderService.GetAllClientTaxiOrders(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)).Result.Data);
    }

    /// <summary>
    /// Сохраняет заказ такси.
    /// </summary>
    /// <param name="taxiOrderViewModel">JSON строка с данными о заказе такси.</param>
    /// <returns>Перенаправление на главную страницу после сохранения заказа.</returns>
    [HttpPost("save-taxi-order")]
    [ProducesResponseType(typeof(TaxiOrder), 200)]
    public async Task<IActionResult> SaveTaxiOrder(string taxiOrderViewModel)
    {
        if (CheckUserStatus()) return RedirectToAction("Logout", "Account", new { area = "" });
        try
        {
            // Десериализация
            var deserializedObject = JsonSerializer.Deserialize<TaxiOrderViewModel>(taxiOrderViewModel);
            TaxiOrder taxiOrder = deserializedObject!.Order;
            taxiOrder.CurrentRoute = deserializedObject.Route;
            taxiOrder.ClientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            await _taxiOrderService.AddTaxiOrder(taxiOrder);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[ClientController.SaveTaxiOrder] error: {ex.Message}");
            return RedirectToAction("CreateTaxiOrder");
        }
    }
}
