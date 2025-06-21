using RechnungenPrivat.ViewModels.AusgabeDetail;

namespace RechnungenPrivat.Views.AusgabeDetail;

public partial class AusgabeDetailView : ContentPage
{
	public AusgabeDetailView(AusgabeDetailViewModel ausgabeDetailViewModel)
	{
		InitializeComponent();
		BindingContext = ausgabeDetailViewModel;
	}

}