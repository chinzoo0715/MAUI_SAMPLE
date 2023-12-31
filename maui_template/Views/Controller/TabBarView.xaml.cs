using maui_template.Services.Navigation;

namespace maui_template.Views.Controller;

public partial class TabBarView : ContentView
{
    MauiNavigationService _navigationService;

    public static readonly BindableProperty ChosenTabProperty =
        BindableProperty.Create(
            nameof(ChosenTab),
            typeof(string),
            typeof(TabBarView),
            string.Empty);

    public string ChosenTab
    {
        get => (string)GetValue(TabBarView.ChosenTabProperty);
        set => SetValue(TabBarView.ChosenTabProperty, value);
    }

    public TabBarView()
    {
        BindingContext = this;
        _navigationService = new MauiNavigationService();
        InitializeComponent();
    }

    async void Home_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await _navigationService.NavigateToAsync("//Main/Home");
    }

    async void Payment_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await _navigationService.NavigateToAsync("//Main/Payment");
    }

    async void Dashboard_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await _navigationService.NavigateToAsync("//Main/Dashboard");
    }

    async void Notification_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await _navigationService.NavigateToAsync("//Main/Notification");
    }
}
