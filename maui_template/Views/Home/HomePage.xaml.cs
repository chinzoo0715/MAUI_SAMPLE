using maui_template.Services.Navigation;
using maui_template.ViewModels;

namespace maui_template.Views.Home;

public partial class HomePage : ContentPage
{
    HomeViewModel _viewModel;
    INavigationService _navigationService;
    bool _isSideBarOpened;

    public HomePage(HomeViewModel viewModel, INavigationService navigationService)
    {
        BindingContext = _viewModel = viewModel;
        _navigationService = navigationService;
        InitializeComponent();
    }

    //Blocks back button in home screen
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    protected override async void OnAppearing()
    {
        App.OnUserInteraction();
        await _viewModel.LoadCustomerData();
    }

    async void Menu_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
        //Special case for android devices
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            if (!_isSideBarOpened)
            {
                await _navigationService.NavigateToAsync("//Main/Payment");
                await Task.Delay(1000);
                await _navigationService.NavigateToAsync("//Main/Home");
                _isSideBarOpened = true;
            }
        }
    }
}
