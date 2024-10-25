namespace TaxiApp.Application.Abstractions.Select.UserSelect;

internal interface ICarSelect<T> : ISelect<T> where T : Car
{
    Task<T> SelectCarAsync(T car);
}
