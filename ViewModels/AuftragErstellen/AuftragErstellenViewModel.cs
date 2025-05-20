using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.AuftragErstellen
{
    public partial class AuftragErstellenViewModel : ObservableObject
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public AuftragErstellenViewModel(IDatabaseService databaseService,INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;

        }

        [ObservableProperty]
        private int _kundenId;

        [ObservableProperty]
        private string _auftragsname;

        [ObservableProperty]
        private string _auftragsbeschreibung;

        [ObservableProperty]
        private DateTime _auftragsdatum;

        [ObservableProperty]
        private decimal _stunden;

        [ObservableProperty]
        private decimal _stundensatz;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsStundenbasiert))] // Benachrichtigt UI, wenn sich Sichtbarkeit ändert
        private Auftragstyp _selectedAuftragstyp = Auftragstyp.Pauschal;

        public List<Auftragstyp> Auftragstypen { get; } = Enum.GetValues(typeof(Auftragstyp)).Cast<Auftragstyp>().ToList();
        public bool IsStundenbasiert => this.SelectedAuftragstyp == Auftragstyp.Stundenbasiert;


    }
}
