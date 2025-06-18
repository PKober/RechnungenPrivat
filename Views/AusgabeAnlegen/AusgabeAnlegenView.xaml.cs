using RechnungenPrivat.ViewModels.AusgabeAnlegen;

namespace RechnungenPrivat.Views.AusgabeAnlegen;

public partial class AusgabeAnlegenView : ContentPage
{
	public AusgabeAnlegenView(AusgabeAnlegenViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}