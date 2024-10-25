namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Map")]
[ApiController]
[Route("api/[area]/[controller]")]
public class MapController : Controller
{
    /// <summary>
    /// Отображает страницу карты с данными о такси.
    /// </summary>
    /// <returns>Представление страницы карты с данными о заказе такси.</returns>
    [HttpGet("index")]
    [ProducesResponseType(typeof(TaxiOrderViewModel), 200)]
    public IActionResult Index()
    {
        try
        {
            // Десериализация TaxiViewModel
            var deserializedViewModel = JsonSerializer.Deserialize<TaxiOrderViewModel>((string)TempData["taxiOrderViewModel_JSON"]!);

            TaxiOrderViewModel viewModel = deserializedViewModel!;
            if (viewModel is null) return View();

            return View(viewModel);
        }
        catch (Exception)
        {
            return View();
        }
    }
}