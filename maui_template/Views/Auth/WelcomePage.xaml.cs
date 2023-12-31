using maui_template.Services.Navigation;
using maui_template.Views.Controller;

namespace maui_template.Views.Auth;

public partial class WelcomePage : ContentPage
{
    INavigationService _navigationService;

    public WelcomePage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    async void Continue_Clicked(System.Object sender, System.EventArgs e)
    {
        var senderObj = sender as ButtonView;
        senderObj.IsEnable = false;
        await _navigationService.NavigateToAsync("Login");
        senderObj.IsEnable = true;
    }
}
