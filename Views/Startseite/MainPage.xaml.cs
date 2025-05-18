using RechnungenPrivat.ViewModels.Startseite;

namespace RechnungenPrivat.Views.Startseite
{


    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }

}
