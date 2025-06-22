using RechnungenPrivat.ViewModels.AusgabenAnzeigen;

namespace RechnungenPrivat.Views.AusgabenAnzeigen;

public partial class AusgabenAnzeigenView : ContentPage
{
	private readonly AusgabenAnzeigenViewModel _viewModel;
	public AusgabenAnzeigenView(AusgabenAnzeigenViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}


    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.InitializeAsync(); 
    }
}