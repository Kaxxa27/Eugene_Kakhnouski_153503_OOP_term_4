using Microsoft.AspNetCore.Mvc;

namespace TaxiApplication.WEB.Controllers;

public class MapController : Controller
{
	public IActionResult Index()
	{
        LocationLists model = new LocationLists();
        var locations = new List<Locations>()
            {
				new Locations(1, "Jeka","Milui Dom", 53.890219, 27.427219),      
                new Locations(2, "Dasha","Minsk", 53.903243, 27.4265),
                new Locations(3, "Moscow","Russia", 55.756542, 37.614922)
            };
        model.LocationList = locations;
        return View(model);
    }
}
public class Locations
{
    public int LocationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Locations(int locid, string title, string desc, double latitude, double longitude)
    {
        this.LocationId = locid;
        this.Title = title;
        this.Description = desc;
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}

public class LocationLists
{
    public IEnumerable<Locations> LocationList { get; set; }
}