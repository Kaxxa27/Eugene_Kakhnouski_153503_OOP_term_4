using BingMapsRESTToolkit;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using System.Web.Helpers;
using TaxiApplication.BLL.ViewModels;

namespace TaxiApplication.WEB.Controllers;

public class MapController : Controller
{
	// BingMapsKey
	private readonly string BING_MAPS_KEY = "AovVDz7evAPwd8iLhM6pBwRHM3wJD5Z2OJHpYwFeRktRbw5wF5-wYkaf9JycZGHW";
	TaxiOrderService _taxiOrderService { get; set; }

	public MapController(TaxiOrderService taxiOrderService)
	{
		_taxiOrderService = taxiOrderService;
	}

	public IActionResult Index()
	{
		// Десериализация TaxiViewModel
		var deserializedViewModel = JsonSerializer.Deserialize<TaxiOrderViewModel>((string)TempData["taxiOrderViewModel_JSON"]!);
		
		//TaxiOrderViewModel viewModel = deserializedObject! as TaxiOrderViewModel
		TaxiOrderViewModel viewModel = deserializedViewModel!;
	
		if (viewModel is null) return View();
		return View(viewModel);
    }

    [HttpGet]
	public IActionResult CreateTaxiOrder() => View();
	[HttpPost]
	public async Task<IActionResult> CreateTaxiOrder(TaxiOrderViewModel taxiOrderViewModel)
	{
		// Создания запроса к REST API для вычисления маршрута между точками
		var request = new RouteRequest
		{
			BingMapsKey = BING_MAPS_KEY,
			RouteOptions = new BingMapsRESTToolkit.RouteOptions()
			{
				TravelMode = TravelModeType.Driving,
				DistanceUnits = DistanceUnitType.Kilometers
			},
			Waypoints = new List<SimpleWaypoint>
			{	
				new SimpleWaypoint(taxiOrderViewModel.Route.StartLocation),
				new SimpleWaypoint(taxiOrderViewModel.Route.EndLocation)
			}
		};

		//BingMapsRESTToolkit
		var response = await ServiceManager.GetResponseAsync(request);
		var result = response.ResourceSets[0]?.Resources[0] as BingMapsRESTToolkit.Route;

		taxiOrderViewModel.Route.Time = new DateTime().AddSeconds(result!.RouteLegs[0].TravelDuration);
		taxiOrderViewModel.Route.Distance = result.RouteLegs[0].TravelDistance;

		var json = JsonSerializer.Serialize(taxiOrderViewModel);

		TempData["taxiOrderViewModel_JSON"] = json;
		return RedirectToAction("Index");
	}

	[HttpGet]
	public IActionResult GetTaxiOrders()
	{
		return View(_taxiOrderService.GetAllTaxiOrders().Result.Data);
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

			return RedirectToAction("Index", "Home");
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[MapController.SaveTaxiOrder] error: {ex.Message})");
			return RedirectToAction("CreateTaxiOrder", "Map");
		}		
	}
}