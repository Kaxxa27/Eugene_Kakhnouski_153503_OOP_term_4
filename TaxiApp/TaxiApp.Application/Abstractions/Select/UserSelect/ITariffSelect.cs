namespace TaxiApp.Application.Abstractions.Select.UserSelect;

internal interface ITariffSelect<T> : ISelect<T> where T: Tariff
{
    Task<T> SelectTariffAsync(T tariff);
}
