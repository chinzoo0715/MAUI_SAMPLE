using maui_template.ViewModels;

namespace maui_template.Views.Home;

public partial class NotificationPage : ContentPage
{
    public NotificationPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    //Blocks back button in home screen
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
