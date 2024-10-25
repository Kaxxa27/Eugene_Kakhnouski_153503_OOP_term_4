namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Client")]
public class ClientController : Controller
{
	ITaxiOrderService _taxiOrderService { get; set; }
	IMapService _mapService { get; set; }
	public ClientController(ITaxiOrderService taxiOrderService, IMapService mapService)
	{
		_taxiOrderService = taxiOrderService;
		_mapService = mapService;
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
