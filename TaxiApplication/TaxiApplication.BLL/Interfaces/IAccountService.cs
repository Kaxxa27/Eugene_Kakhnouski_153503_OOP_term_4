
using System.Security.Claims;
using TaxiApplication.BLL.ViewModels;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Responce;

namespace TaxiApplication.BLL.Interfaces;

public interface IAccountService
{
    public Task<BaseResponse<ClaimsPrincipal>> LoginAsync(LoginViewModel loginViewModel);
    public Task<BaseResponse<ClaimsPrincipal>> RegistrationAsync(Client client);
}
