using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using RechnungenPrivat.Data.Interfaces;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.PrivateSeiten.PrivateSeiteStarteSeite
{
    public partial class PrivateSeiteStarteSeiteViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        //private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private int _selectedAusgabeId;
        [ObservableProperty]
        private Chart ausgabenDiagramm;
        public PrivateSeiteStarteSeiteViewModel(IDatabaseService databaseService, /*INavigationService navigationService ,*/IDialogService dialogService)
        {
            _databaseService = databaseService;
            //    _navigationService = navigationService;
            _dialogService = dialogService;
            ErstelleDiagramm();
        }


        private void ErstelleDiagramm()
        {
            // 1. Die Datenpunkte (Einträge) definieren
            var entries = new[]
            {
            new ChartEntry(200f)
            {
                Label = "Miete",
                ValueLabel = "200 €",
                Color = SKColor.Parse("#266489")
            },
            new ChartEntry(400f)
            {
                Label = "Essen",
                ValueLabel = "400 €",
                Color = SKColor.Parse("#68B9C0")
            },
            new ChartEntry(150f)
            {
                Label = "Freizeit",
                ValueLabel = "150 €",
                Color = SKColor.Parse("#90D585")
            }
            };

            // 2. Das Diagramm-Objekt erstellen und konfigurieren
            AusgabenDiagramm = new BarChart
            {
                Entries = entries,
                LabelTextSize = 40,
                BackgroundColor = SKColors.Transparent,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal
            };
        }
    }
}
