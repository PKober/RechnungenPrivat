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
        public async Task TestErstelleRechnungasync(CancellationToken cancellationToken)
        {
            Kunde kunde = new Kunde()
            {
                KundenName = "PascalssssKober",
                KundenAdresse = @"Irgendwo Im Nirgendwo
ansonsten zuahuse"
            };

            List<Auftrag> auftrags = new List<Auftrag>()
            {
                new Auftrag()
                {
                    Auftragsname =  "Treppenhausreinigung",
                    Typ = Auftragstyp.Pauschal,
                    Betrag = 115,

                },
                new Auftrag()
                {
                    Auftragsname =  "Vorhof",
                    Typ = Auftragstyp.Pauschal,
                    Betrag = 152,
                },
                new Auftrag()
                {
                    Auftragsname =  "Ferienwohnung",
                    Typ = Auftragstyp.Stundenbasiert,
                    Stunden = 5,
                    Stundensatz = (decimal)22.50,
                    Betrag = (decimal)5 * (decimal)22.50

                }
            };


            byte[] wordData = await _rechnungsService.ErstelleRechnungWordAsync(kunde, auftrags);

            using var stream = new MemoryStream(wordData);
            var fileSSaverResult = await FileSaver.Default.SaveAsync("Test.docx", stream);
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
