using maui_template.ViewModels;

namespace maui_template.Views.Home;

public partial class HomePage : ContentPage
{
    HomeViewModel _viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
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

    void Menu_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}
