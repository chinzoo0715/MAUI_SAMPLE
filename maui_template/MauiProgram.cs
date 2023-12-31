using CommunityToolkit.Maui;
using maui_template.Services.AppEnvironment;
using maui_template.Services.Auth;
using maui_template.Services.Home;
using maui_template.Services.Navigation;
using maui_template.Services.RequestProvider;
using maui_template.ViewModels;
using maui_template.Views.Auth;
using maui_template.Views.Home;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace maui_template;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("SFProDisplay-Regular.ttf", "SFRegular");
                fonts.AddFont("SFProDisplay-Medium.ttf", "SFMedium");
                fonts.AddFont("SFProDisplay-Bold.ttf", "SFBold");
            })
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .RegisterViewModels()
            .RegisterViews()
            .RegisterAppServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
        mauiAppBuilder.Services.AddSingleton<IHomeService, HomeService>();
        mauiAppBuilder.Services.AddSingleton<IAppEnvironmentService, AppEnvironmentService>(
            serviceProvider =>
            {
                var requestProvider = serviceProvider.GetService<IRequestProvider>();
                var aes =
                    new AppEnvironmentService(
                        new AuthService(requestProvider),
                        new HomeService(requestProvider));

                aes.UpdateDependencies();
                return aes;
            });
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AuthViewModel>();
        mauiAppBuilder.Services.AddSingleton<HomeViewModel>();
        mauiAppBuilder.Services.AddSingleton<BaseViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<WelcomePage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddTransient<HomePage>();
        mauiAppBuilder.Services.AddTransient<PaymentPage>();
        mauiAppBuilder.Services.AddTransient<DashboardPage>();
        mauiAppBuilder.Services.AddTransient<NotificationPage>();
        return mauiAppBuilder;
    }
}

