using RechnungenPrivat.ViewModels.KundenAnzeigen;

namespace RechnungenPrivat.Views.KundenAnzeigen;

public partial class KundenAnzeigenView : ContentPage
{

    public KundenAnzeigenView(KundenAnzeigenViewModel viewModel)
    {

        InitializeComponent();
        BindingContext = viewModel;
        
    }
}