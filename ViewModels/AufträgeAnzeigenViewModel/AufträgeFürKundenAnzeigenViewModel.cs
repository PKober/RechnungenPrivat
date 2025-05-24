using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.AufträgeFürKundenAnzeigenViewModel
{
    [QueryProperty(nameof(KundenId), "KundenId")]
    public partial class AufträgeFürKundenAnzeigenViewModel : ObservableObject
    {

        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;

        public AufträgeFürKundenAnzeigenViewModel(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            Aufträge = new ObservableCollection<Auftrag>();
        }
        [ObservableProperty]
        private int _kundenId;

        [ObservableProperty]
        private ObservableCollection<Auftrag> _aufträge;

        [ObservableProperty]
        private string _kundenName;

        [ObservableProperty]
        private Auftrag _selectedAutrag;

        [ObservableProperty]
        private bool _auftragGewählt = false;

        [ObservableProperty]
        private string _auftrasbeschreibung;

        partial void OnKundenIdChanged(int value)
        {
            _ = LoadAufträgeAsync(value);
        }
        public async Task LoadAufträgeAsync(int kundenId)
        {
            var aufträgeListe = await _databaseService.GetAllAuftraegeByKundeIdAsync(KundenId);
            if (aufträgeListe != null)
            {
                Aufträge.Clear();
                foreach (var auftrag in aufträgeListe)
                {
                    Aufträge.Add(auftrag);
                }
            }
        }

        [RelayCommand]
        private async Task GoToAufträgeDetails(Auftrag auftragParameter)
        {
            Auftrag auftragFürAktion = null;

            if (auftragParameter != null)
            {
                auftragFürAktion = auftragParameter;
            }
            else if (SelectedAutrag != null)
            {
                auftragFürAktion = SelectedAutrag;
            }

            if (auftragFürAktion == null)
                return;
            AuftragGewählt = true;

           // await Shell.Current.DisplayAlert("Ausgewählter Auftrag",
           //     $"Name: {auftragFürAktion.Auftragsname}\nBeschreibung: {auftragFürAktion.Beschreibung}", "OK");

        }

        [RelayCommand]
        private async Task DeleteAuftragAsync()
        {
            if (SelectedAutrag == null)
            {
                await Shell.Current.DisplayAlert("Fehler", "Bitte wählen Sie einen Auftrag zum Löschen aus.", "OK");
                return;
            }
            var result = await Shell.Current.DisplayAlert("Bestätigung",
                $"Möchten Sie den Auftrag '{SelectedAutrag.Auftragsname}' wirklich löschen?", "Ja", "Nein");
            if (result)
            {
                await _databaseService.DeleteAuftragAsync(SelectedAutrag);
                Aufträge.Remove(SelectedAutrag);
                SelectedAutrag = null;
                AuftragGewählt = false;
            }

        }
    }
}
