using Microsoft.Extensions.Logging;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Navigation;
using RechnungenPrivat.ViewModels.Startseite;
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
		builder.Services.AddSingleton<INavigationService,MauiNavigationService>();
		builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
