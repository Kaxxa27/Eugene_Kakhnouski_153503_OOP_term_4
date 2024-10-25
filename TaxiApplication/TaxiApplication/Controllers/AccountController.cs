using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaxiApplication.WEB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    #region HttpGet
    /// <summary>
    /// Отображает страницу для входа в систему.
    /// </summary>
    /// <returns>Представление для входа.</returns>
    [HttpGet("login")]
    [ProducesResponseType(200)]
    public IActionResult Login() => View();

    /// <summary>
    /// Отображает страницу регистрации.
    /// </summary>
    /// <returns>Представление для регистрации.</returns>
    [HttpGet("registration")]
    [ProducesResponseType(200)]
    public IActionResult Registration() => View();
    #endregion

    #region HttpPost
    /// <summary>
    /// Выполняет вход в систему.
    /// </summary>
    /// <param name="loginViewModel">Модель данных для входа.</param>
    /// <returns>Редирект на главную страницу, если вход успешен; иначе, возвращает страницу входа с ошибками.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginViewModel), 200)]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.LoginAsync(loginViewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignInAsync(response.Data!);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }

        return View(loginViewModel);
    }

    /// <summary>
    /// Выполняет регистрацию нового клиента.
    /// </summary>
    /// <param name="client">Модель данных клиента для регистрации.</param>
    /// <returns>Редирект на главную страницу, если регистрация успешна; иначе, возвращает страницу регистрации с ошибками.</returns>
    [HttpPost("registration")]
    [ProducesResponseType(typeof(Client), 200)]
    public async Task<IActionResult> Registration(Client client) 
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.RegistrationAsync(client);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignInAsync(response.Data!);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }

        return View(client);
    }
    #endregion

    /// <summary>
    /// Возвращает страницу доступа запрещен.
    /// </summary>
    /// <returns>Статус 403 Forbidden.</returns>
    [HttpGet("access-denied")]
    [ProducesResponseType(403)]
    public IActionResult AccessDenied() => StatusCode(403, "Forbidden");

    /// <summary>
    /// Выходит из системы и редиректит на главную страницу.
    /// </summary>
    /// <returns>Редирект на главную страницу.</returns>
    [HttpPost("logout")]
    [ProducesResponseType(typeof(RedirectToActionResult), 200)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}