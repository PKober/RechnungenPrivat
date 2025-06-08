using RechnungenPrivat.Data.Interfaces;

namespace RechnungenPrivat.Navigation
{
    class MauiNavigationService : INavigationService
    {
        public Task NavigateToAsync(string route)
        {
            return Shell.Current.GoToAsync(route);
        }

        public Task NavigateToAsync<T>(string route, IDictionary<string, object> parameters)
        {
            return Shell.Current.GoToAsync(route, parameters);
        }

        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }

}

