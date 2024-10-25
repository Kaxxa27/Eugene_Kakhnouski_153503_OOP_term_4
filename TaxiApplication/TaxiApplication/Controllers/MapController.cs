using Microsoft.AspNetCore.Mvc;

namespace TaxiApplication.WEB.Controllers;

public class MapController : Controller
{
	public IActionResult Index()
	{
        LocationLists model = new LocationLists();
        var locations = new List<Locations>()
            {
                //53.890219, 27.427219
                new Locations(1, "Dom","Milui Dom", 53.890219, 27.427219),
                new Locations(2, "Dom","Milui Dom",27.427219,53.890219),
                //new Locations(1, "Bhubaneswar","Bhubaneswar, Odisha", 20.296059, 85.824539),
                //new Locations(2, "Hyderabad","Hyderabad, Telengana", 17.387140, 78.491684),
                //new Locations(3, "Bengaluru","Bengaluru, Karnataka", 12.972442, 77.580643)
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