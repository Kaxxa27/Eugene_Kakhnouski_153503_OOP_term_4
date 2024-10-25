using Microsoft.AspNetCore.Mvc;
using TaxiApplication.Domain;

namespace TaxiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IUnitOfWork _repository;

    public HomeController(IUnitOfWork repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Отображает главную страницу приложения.
    /// </summary>
    /// <returns>Представление главной страницы.</returns>
    [HttpGet("")]
    [ProducesResponseType( 200)]
    public IActionResult Index() => View();

    /// <summary>
    /// Отображает страницу для добавления нового элемента.
    /// </summary>
    /// <returns>Представление для добавления элемента.</returns>
    [HttpGet("add-item")]
    [ProducesResponseType( 200)]
    public IActionResult AddItem() => View();
}