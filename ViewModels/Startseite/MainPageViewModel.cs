using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.KundenAnzeigen;
using RechnungenPrivat.Views.KundenLöschen;

namespace RechnungenPrivat.ViewModels.Startseite
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public async Task GoToKundenAnlegen()
        {
            var route = $"{nameof(KundenAnlegenView)}";
            await _navigationService.NavigateToAsync(route);
        }

        [RelayCommand]
        public async Task GoToKundenLöschen()
        {
            var route = $"{nameof(KundenLöschenView)}";
            await _navigationService.NavigateToAsync(route);
        }

        [RelayCommand]
        public async Task GoToKundenAnzeigen()
        {
            var route = $"{nameof(KundenAnzeigenView)}";
            await _navigationService.NavigateToAsync(route);
        }
    }
}
