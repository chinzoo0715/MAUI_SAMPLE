using System.Diagnostics;
using System.Timers;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using maui_template.Constants;
using maui_template.Models;
using maui_template.Services.AppEnvironment;
using maui_template.Services.Navigation;

namespace maui_template;

public partial class App : Application
{
    private readonly IAppEnvironmentService _appEnvironmentService;

    private static Stopwatch sessionTimer;
    private static TimeSpan sessionTimeoutDuration = TimeSpan.FromMinutes(GlobalValues.SESSION_TIMEOUT);
    private static System.Timers.Timer sessionTimeoutTimer;

    private static INavigationService _navigationService;

    public App(IAppEnvironmentService appEnvironmentService, INavigationService navigationService)
    {
        _appEnvironmentService = appEnvironmentService;
        _navigationService = navigationService;

        //Choosing for right safe area display
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS && !GlobalValues.NON_NOTCH_IPHONE_MODELS.Contains(DeviceInfo.Current.Name))
        {
            GlobalValues.MARGIN_TOP = -60;
            GlobalValues.MARGIN_BOTTOM = -35;
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS && GlobalValues.NON_NOTCH_IPHONE_MODELS.Contains(DeviceInfo.Current.Name))
        {
            GlobalValues.MARGIN_TOP = -30;
        }

        InitializeComponent();
        InitApp();
        MainPage = new AppShell();

        // Initialize session timeout timer
        sessionTimer = new Stopwatch();
        sessionTimeoutTimer = new System.Timers.Timer(sessionTimeoutDuration.TotalMilliseconds);
        sessionTimeoutTimer.Elapsed += OnSessionTimeoutElapsed;
        sessionTimeoutTimer.AutoReset = false;
    }

    private void InitApp()
    {
        _appEnvironmentService.UpdateDependencies();
    }

    public static void StartSession()
    {
        sessionTimer.Restart();
        ResetSessionTimeout();
    }

    public static void StopSession()
    {
        sessionTimer.Stop();
        sessionTimeoutTimer.Stop();
    }

    private static void ResetSessionTimeout()
    {
        sessionTimeoutTimer.Stop();
        sessionTimeoutTimer.Start();
    }

    public static void OnUserInteraction()
    {
        if (!string.IsNullOrEmpty(GlobalValues.ACCESS_TOKEN))
            ResetSessionTimeout();
    }

    private void OnSessionTimeoutElapsed(object sender, ElapsedEventArgs e)
    {
        MoveToLogin();
    }

    protected override void OnSleep()
    {
        StopSession();
        base.OnSleep();
    }

    protected override void OnResume()
    {
        StartSession();
        base.OnResume();
    }

    public static void MoveToLogin(bool isShowDialog = true)
    {
        // Session timeout logic here
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            StopSession();
            if (isShowDialog && !string.IsNullOrEmpty(GlobalValues.ACCESS_TOKEN))
                await ShowToast("Your access has expired");
            GlobalValues.ACCESS_TOKEN = "";

            await _navigationService.NavigateToAsync("//Welcome");
        });
    }

    public static async Task ShowToast(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        string text = message;
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 12;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    public static async Task<bool> CheckResponse(object data, bool isMarket = false, bool isLogged = true)
    {
        IResponse response = data as IResponse;

        if (response == null || response.int_result != 0)
        {
            string errorMessage = response?.int_result > 0 ? response.str_result : "There is an error. Try again later";
            await ShowToast(errorMessage);
            return false;
        }

        return true;
    }
}

