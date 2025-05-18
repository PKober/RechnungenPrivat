using CommunityToolkit.Mvvm.ComponentModel;
using RechnungenPrivat.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.KundeLöschen
{
    public class KundenLöschenViewModel : ObservableObject
    {
        
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;
        public KundenLöschenViewModel(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }
        
        [ObservableProperty]
        private string _kundename;
        [ObservableProperty]
        private string _kundenadresse;


    }
}
