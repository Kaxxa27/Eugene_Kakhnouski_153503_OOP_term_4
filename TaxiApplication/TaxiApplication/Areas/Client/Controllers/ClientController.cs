namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Client")]
public class ClientController : Controller
{
	ITaxiOrderService _taxiOrderService { get; set; }
	IMapService _mapService { get; set; }
	IClientService _clientService { get; set; }
	public ClientController(ITaxiOrderService taxiOrderService, IMapService mapService, IClientService clientService)
	{
		_taxiOrderService = taxiOrderService;
		_mapService = mapService;
		_clientService = clientService;
	}


    [HttpGet]
    public async Task<IActionResult> ProfileEdit()
	{
	   var client = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data;
       return View(client);
    }	

	[HttpPost]
	public async Task<IActionResult> ProfileEdit(Client client)
	{
		client.Profile.Photo = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data!.Profile.Photo;
        var response = await _clientService.UpdateClient(client);

        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(client);
        }
        return View(client/*Страница ошибки*/);
    }

    [HttpPost]
    public async Task<IActionResult> UploadPhoto(IFormFile photoFile)
    {
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
            await Console.Out.WriteLineAsync($"[ClientController.UploadPhoto] error: {ex.Message})");
            return RedirectToAction("ProfileEdit");
        }
    }


    [HttpGet]
	public IActionResult CreateTaxiOrder() => View();

	[HttpPost]
	public async Task<IActionResult> CreateTaxiOrder(TaxiOrderViewModel taxiOrderViewModel)
	{
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
			await Console.Out.WriteLineAsync($"[ClientController.CreateTaxiOrder] error: {ex.Message})");
			return RedirectToAction("CreateTaxiOrder");
		}
	}

	[HttpGet]
	public IActionResult GetTaxiOrders()
	{
		return View(_taxiOrderService.GetAllClientTaxiOrders(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)).Result.Data);
	}

	[HttpPost]
	public async Task<IActionResult> SaveTaxiOrder(string taxiOrderViewModel)
	{
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
			await Console.Out.WriteLineAsync($"[ClientController.SaveTaxiOrder] error: {ex.Message})");
			return RedirectToAction("CreateTaxiOrder");
		}
	}
}
