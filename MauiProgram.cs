using Microsoft.Extensions.Logging;
using RechnungenPrivat.Data.Datenbank;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Navigation;
using RechnungenPrivat.ViewModels.KundenAnlegen;
using RechnungenPrivat.ViewModels.Startseite;
using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.Startseite;

namespace RechnungenPrivat;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        // Register the database service
		builder.Services.AddSingleton<IDatabaseService,DatabaseService>();

        // Register the navigation service
        builder.Services.AddSingleton<INavigationService,MauiNavigationService>();

        // Register the ViewModels
        builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<KundenAnlegenViewModel>();

        // Register the Views
        builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<KundenAnlegenView>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
