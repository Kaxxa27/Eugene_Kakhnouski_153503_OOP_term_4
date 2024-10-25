namespace TaxiApp.Application.Abstractions.UserAbstractionsService;

internal interface IUserService : ICancelOrder, IContactTechnicalSupport
{
    Task<Order> CreateOrder();


    
}
