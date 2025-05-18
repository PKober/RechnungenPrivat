using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.KundenAnlegen
{
    public partial class KundenAnlegenViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _kundenname;

        [ObservableProperty]
        private string _kundenadresse;

    }
}
