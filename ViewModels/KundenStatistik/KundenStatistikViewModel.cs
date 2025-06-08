using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace RechnungenPrivat.ViewModels.KundenStatistik
{
    [QueryProperty(nameof(KundenId), "KundenId")]
    public partial class KundenStatistikViewModel : ObservableObject
    {

        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public KundenStatistikViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            Aufträge = new ObservableCollection<Auftrag>();
            

        }

        [ObservableProperty]
        private int _kundenId;
        [ObservableProperty]
        private string _kundenName;
        [ObservableProperty]
        private int _anzahlAufträge;
        [ObservableProperty]
        private decimal _gesamtBetrag;
        [ObservableProperty]
        private decimal _durchschnittBetrag;
        [ObservableProperty]
        private decimal _durchschnittStunden;
        [ObservableProperty]
        private ObservableCollection<Auftrag> _aufträge;
        [ObservableProperty]
        private ObservableCollection<Auftrag> _gefilterteAufträge = new();

        private List<Auftrag> _alleKundenAufträge = new();
        private Kunde _kunde;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ApplyFilterCommand))]
        private int? _selectedJahr;

        [ObservableProperty]
        // [NotifyCanExecuteChangedFor(nameof())]
        private MonatItem _selectedMonatItem;

        public ObservableCollection<int?> Jahre { get; } = new();
        
        public ObservableCollection<MonatItem> Monate { get; } = new();

        partial void OnKundenIdChanged(int value)
        {
            _ = LoadAufträgeAsync(value);
            _ = LoadKundenDetailsAsync(value);
            InitializeFilterData();

            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedJahr) || e.PropertyName == nameof(SelectedMonatItem))
                {
                    ApplyFilter();
                }
            };

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
                    GesamtBetrag += auftrag.Betrag;
                    DurchschnittStunden += (decimal)auftrag.Stunden;
                    
                }
                DurchschnittBetrag = GesamtBetrag / Aufträge.Count - 1;
                
                AnzahlAufträge = Aufträge.Count;

            }
        }
        public async Task LoadKundenDetailsAsync(int kundenId)
        {
            var kunde = await _databaseService.GetKundeByIdAsync(kundenId);
            if (kunde != null)
            {
                _kunde = kunde;
                KundenName = kunde.KundenName;
            }
        }

        public class MonatItem
        {
            public string Name { get; set; }
            public int? Wert { get; set; } // Nullable für "Alle" Option
        }

        private void InitializeFilterData()
        {
            Monate.Add(new MonatItem
            {
                Name = "Alle Monate",
                Wert = null
            });
            for (int i = 1; i <= 12; i++)
            {
                Monate.Add(new MonatItem
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                    Wert = i
                });
            }

            Jahre.Add(null);
            int aktuellesJahr = DateTime.Now.Year;
            for (int i = aktuellesJahr; i >= aktuellesJahr - 10  ; i--)
            {
                Jahre.Add(i);
            }

            SelectedMonatItem = Monate.FirstOrDefault();
            SelectedJahr = Jahre.FirstOrDefault(1);
        }

        [RelayCommand]
        private void ApplyFilter()
        {
            if (Aufträge == null)
            {
                return;
            }

            IEnumerable<Auftrag> tempGefilterteAufträge = Aufträge;

            if (SelectedJahr.HasValue)
            {
                tempGefilterteAufträge = tempGefilterteAufträge.Where(a => a.Auftragsdatum.HasValue && a.Auftragsdatum.Value.Year == SelectedJahr.Value);
            }

            if (SelectedMonatItem?.Wert.HasValue ?? false)
            {
                tempGefilterteAufträge = tempGefilterteAufträge.Where(a => a.Auftragsdatum.HasValue && a.Auftragsdatum.Value.Month == SelectedMonatItem.Wert.Value);
            }

            GefilterteAufträge.Clear();
            foreach (var auftrag in tempGefilterteAufträge.OrderByDescending(a => a.Auftragsdatum))
            {
                GefilterteAufträge.Add(auftrag);
            }
        }
    }
}
