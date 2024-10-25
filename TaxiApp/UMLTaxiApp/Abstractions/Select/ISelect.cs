
namespace TaxiApp.Application.Abstractions.Select;

internal interface ISelect<T> where T : Entity
{
    Task<T> SelectAsyncId(int id);
}
