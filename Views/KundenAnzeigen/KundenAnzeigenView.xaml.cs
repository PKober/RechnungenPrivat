using RechnungenPrivat.ViewModels.KundenAnzeigen;

namespace RechnungenPrivat.Views.KundenAnzeigen;

public partial class KundenAnzeigenView : ContentPage
{
    private readonly KundenAnzeigenViewModel _viewModel;
    public KundenAnzeigenView(KundenAnzeigenViewModel viewModel)
    {

        InitializeComponent();
        BindingContext = viewModel;
        viewModel.LoadKundenAsync();
        _viewModel = viewModel;
    }
}