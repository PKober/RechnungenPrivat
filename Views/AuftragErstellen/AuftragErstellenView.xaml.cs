using RechnungenPrivat.ViewModels.AuftragErstellen;

namespace RechnungenPrivat.Views.AuftragErstellen;

public partial class AuftragErstellenView : ContentPage
{
    public AuftragErstellenView(AuftragErstellenViewModel auftragErstellenViewModel)
    {
        InitializeComponent();
        BindingContext = auftragErstellenViewModel;
    }
}