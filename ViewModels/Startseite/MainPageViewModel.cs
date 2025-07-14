using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.AusgabenAnzeigen;
using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.KundenAnzeigen;
using RechnungenPrivat.Views.KundenLöschen;
using System.Runtime.InteropServices;
using System.Threading;

namespace RechnungenPrivat.ViewModels.Startseite
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IRechnungsService _rechnungsService;
        public MainPageViewModel(INavigationService navigationService, IRechnungsService rechnungsService)
        {
            _navigationService = navigationService;
            _rechnungsService = rechnungsService;
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

        [RelayCommand]
        public async Task GoToAusgabenVerwalten()
        {
            var route = $"{nameof(AusgabenAnzeigenView)}";
            await _navigationService.NavigateToAsync(route); 
        }
    }
}
