using maui_template.ViewModels;

namespace maui_template.Views.Home;

public partial class PaymentPage : ContentPage
{
    public PaymentPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}
