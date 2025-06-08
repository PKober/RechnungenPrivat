namespace RechnungenPrivat.Data.Interfaces
{
    public interface INavigationService
    {

        Task NavigateToAsync(string route);
        Task NavigateToAsync<T>(string route, IDictionary<string, object> parameters);
        Task GoBackAsync();

    }
}
