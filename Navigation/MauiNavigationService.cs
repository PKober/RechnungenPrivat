using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.ViewModels;

namespace RechnungenPrivat.Navigation
{
    class MauiNavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public MauiNavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task NavigateToAsync(string route)
        {
           
            await Shell.Current.GoToAsync(route);
            await InitializeViewModel(route);
        }

        public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
        {
            await InitializeViewModel(route, parameters);
            await Shell.Current.GoToAsync(route, parameters);
        }

        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        private async Task InitializeViewModel(string route, object? parameter = null)
        {
            var page = Shell.Current.CurrentPage;
            if(page?.BindingContext is BaseViewModel viewModel)
            {
                await viewModel.InitializeAsync(parameter);
            }
        }
    }

}

