using RechnungenPrivat.ViewModels.AufträgeFürKundenAnzeigenViewModel;

namespace RechnungenPrivat.Views.AufträgeFürKundenAnzeigen;

public partial class AufträgeFürKundenAnzeigenView : ContentPage
{
    public AufträgeFürKundenAnzeigenView(AufträgeFürKundenAnzeigenViewModel aufträgeFürKundenAnzeigenViewModel)
    {
        InitializeComponent();
        BindingContext = aufträgeFürKundenAnzeigenViewModel;
    }

}