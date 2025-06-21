
using Microsoft.Extensions.DependencyInjection.Extensions;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RechnungenPrivat.Data.Datenbank;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Data.Services;
using RechnungenPrivat.Navigation;
using RechnungenPrivat.ViewModels.AufträgeFürKundenAnzeigenViewModel;
using RechnungenPrivat.ViewModels.AuftragErstellen;
using RechnungenPrivat.ViewModels.AusgabeAnlegen;
using RechnungenPrivat.ViewModels.AusgabeDetail;
using RechnungenPrivat.ViewModels.AusgabenAnzeigen;
using RechnungenPrivat.ViewModels.KundenAnlegen;
using RechnungenPrivat.ViewModels.KundenAnzeigen;
using RechnungenPrivat.ViewModels.KundenLöschen;
using RechnungenPrivat.ViewModels.KundenStatistik;
using RechnungenPrivat.ViewModels.Startseite;
using RechnungenPrivat.Views.AufträgeFürKundenAnzeigen;
using RechnungenPrivat.Views.AuftragErstellen;
using RechnungenPrivat.Views.AusgabeAnlegen;
using RechnungenPrivat.Views.AusgabeDetail;
using RechnungenPrivat.Views.AusgabenAnzeigen;
using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.KundenAnzeigen;
using RechnungenPrivat.Views.KundenLöschen;
using RechnungenPrivat.Views.KundenStatistik;
using RechnungenPrivat.Views.Startseite;


namespace RechnungenPrivat;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register the Dialog Service 
        builder.Services.AddSingleton<IDialogService, DialogService>();

        // Register the database service
        builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

        // Register the navigation service
        builder.Services.AddSingleton<INavigationService>(provider => new MauiNavigationService(provider));

        // Register the Excel Export Service
        builder.Services.AddSingleton<IExcelExportService, ExcelExportService>();

        // Register the Media Serice 
        builder.Services.AddSingleton<IMediaService, MediaService>();

        // Register the ViewModels
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<KundenAnlegenViewModel>();
        builder.Services.AddTransient<KundenLöschenViewModel>();
        builder.Services.AddTransient<KundenAnzeigenViewModel>();
        builder.Services.AddTransient<AuftragErstellenViewModel>();
        builder.Services.AddTransient<AufträgeFürKundenAnzeigenViewModel>();
        builder.Services.AddTransient<KundenStatistikViewModel>();
        builder.Services.AddTransient<AusgabeAnlegenViewModel>();
        builder.Services.AddTransient<AusgabenAnzeigenViewModel>();
        builder.Services.AddTransient<AusgabeDetailViewModel>();

        // Register the Views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<KundenAnlegenView>();
        builder.Services.AddTransient<KundenLöschenView>();
        builder.Services.AddTransient<KundenAnzeigenView>();
        builder.Services.AddTransient<AuftragErstellenView>();
        builder.Services.AddTransient<AufträgeFürKundenAnzeigenView>();
        builder.Services.AddTransient<KundenStatistikView>();
        builder.Services.AddTransient<AusgabeAnlegenView>();
        builder.Services.AddTransient<AusgabenAnzeigenView>();
        builder.Services.AddTransient<AusgabeDetailView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
