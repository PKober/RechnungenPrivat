using RechnungenPrivat.ViewModels.KundenAnlegen;

namespace RechnungenPrivat.Views.KundenAnlegen;

public partial class KundenAnlegenView : ContentPage
{
    public KundenAnlegenView(KundenAnlegenViewModel kundenAnlegenViewModel)
    {
        InitializeComponent();
        BindingContext = kundenAnlegenViewModel;
    }
}