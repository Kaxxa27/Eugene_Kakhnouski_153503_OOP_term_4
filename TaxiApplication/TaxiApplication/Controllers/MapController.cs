using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace TaxiApplication.WEB.Controllers;

public class MapController : Controller
{
	LocationLists model = new LocationLists();
    Locations CurrentLocation = new Locations();

    public IActionResult Index(Locations location)
    {
        return View(location);
    }

    [HttpGet]
    public IActionResult BuildRoute() => PartialView();

	[HttpPost]
    public IActionResult BuildRoute(Locations location)
    {
		return RedirectToAction("Index", location);
		//if (loc.Start != null && loc.End != null)
        //    return RedirectToAction("Index", loc);
        //return PartialView();    
    }
}
public class Locations
{
    public int LocationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Start { get; set; }
    public string End { get; set; }

    public Locations(int locid, string title, string desc, double latitude, double longitude)
    {
        this.LocationId = locid;
        this.Title = title;
        this.Description = desc;
        this.Latitude = latitude;
        this.Longitude = longitude;
    }

    public Locations()
    {
        
    }
}

public class LocationLists
{
    public IEnumerable<Locations> LocationList { get; set; }
}