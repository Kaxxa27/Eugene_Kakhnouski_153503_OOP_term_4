using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TaxiApplication.Domain.Entity;

namespace TaxiApplication.WEB.Controllers;

public class MapController : Controller
{
    TaxiOrderService _taxiOrderService { get; set; }

    public MapController(TaxiOrderService taxiOrderService)
    {
		_taxiOrderService = taxiOrderService;
	}
    public IActionResult Index(TaxiOrder taxiOrder)
	{
        var CurrentOrder = _taxiOrderService.GetOrder(taxiOrder.Id).Result.Data;
        if (CurrentOrder is null) return View();
        return View(CurrentOrder);  
    }

    [HttpGet]
	public IActionResult CreateTaxiOrder() => PartialView();
	[HttpPost]
	public IActionResult CreateTaxiOrder(TaxiOrder taxiOrder)
	{
		if (ModelState.IsValid)
		{
			var response = _taxiOrderService.AddTaxiOrder(taxiOrder).Result;

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("Index", taxiOrder);
			}

			ModelState.AddModelError("", response.Description);
		}
		return View();
		//_taxiOrderService.AddTaxiOrder(taxiOrder);
		//return RedirectToAction("Index",taxiOrder);    
	}
	[HttpGet]
    public IActionResult BuildRoute() => PartialView();

	[HttpPost]
    public IActionResult BuildRoute(TaxiApplication.Domain.Entity.Route.Route route)
    {
        TaxiOrder taxiOrder = new TaxiOrder();
        taxiOrder.CurrentRoute = route;
		return RedirectToAction("Index", taxiOrder);  
    }

	[HttpGet]
	public IActionResult GetTaxiOrders()
	{
		return View(_taxiOrderService.GetAllOrders().Result.Data);
	}
}