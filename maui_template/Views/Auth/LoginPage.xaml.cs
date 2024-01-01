using System.ComponentModel;
using maui_template.Constants;
using maui_template.Models;
using maui_template.Services.Navigation;
using maui_template.ViewModels;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace maui_template.Views.Auth;

public partial class LoginPage : ContentPage
{
    INavigationService _navigationService;
    private AuthViewModel _viewModel;

    private bool _isFingerprintAvailable = false;

    public LoginPage(INavigationService navigationService, AuthViewModel viewModel)
    {
        _navigationService = navigationService;
        BindingContext = _viewModel = viewModel;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        InitializeComponent();
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthViewModel.IsBusy))
        {
            LoginBtn.IsBlock = _viewModel.IsBusy;
            RegisterBtn.IsEnabled = !_viewModel.IsBusy;
            UsernameTxt.IsBlock = _viewModel.IsBusy;
            PasswordTxt.IsBlock = _viewModel.IsBusy;
            FingerPrintCheckboxBtn.IsEnabled = !_viewModel.IsBusy;
            FingerprintBtn.IsEnabled = !_viewModel.IsBusy;
            ForgotPassBtn.IsEnabled = !_viewModel.IsBusy;
        }
    }

    protected override async void OnAppearing()
    {
        _isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);

        if (!_isFingerprintAvailable)
        {
            FingerPrintCheckboxBtn.IsVisible = false;
            FingerprintBtn.IsVisible = false;
        }
        else
        {
            AuthenticationType fingerprintType = await CrossFingerprint.Current.GetAuthenticationTypeAsync();
            if (fingerprintType == AuthenticationType.Face)
            {
                FingerPrintImage.Source = "faceid";
                _viewModel.fingerPrintType = "Face ID";
                FingerPrintCheckBoxTxt.Text = "Login with FaceID next time?";
            }
            else if (fingerprintType == AuthenticationType.Fingerprint)
            {
                FingerPrintImage.Source = "fingerprint";
                _viewModel.fingerPrintType = "Fingerprint";
                FingerPrintCheckBoxTxt.Text = "Login with Fingerprint next time?";
            }

            FingerPrintCheckboxBtn.IsVisible = true;
            FingerprintBtn.IsVisible = true;

            if (SettingsValues.Instance.IsFingerPrintEnabled != "Y")
            {
                FingerprintBtn.IsVisible = false;
            }
            else
            {
                FingerPrintCheckboxBtn.IsVisible = false;
            }
        }

        UsernameTxt.Text = SettingsValues.Instance.UserName;
        PasswordTxt.Text = string.Empty;
    }

    async void Back_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var senderObj = sender as StackLayout;
        senderObj.IsEnabled = false;
        await _navigationService.PopAsync();
        senderObj.IsEnabled = true;
    }

    void ForgotPassword_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
    }

    void Register_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
    }

    async void Login_Clicked(System.Object sender, System.EventArgs e)
    {
        bool isValid = true;
        if (string.IsNullOrEmpty(UsernameTxt.Text))
        {
            UsernameTxt.ErrText = "Insert your username";
            isValid = false;
        }
        else
        {
            UsernameTxt.ErrText = "";
        }

        if (string.IsNullOrEmpty(PasswordTxt.Text))
        {
            PasswordTxt.ErrText = "Insert your password";
            isValid = false;
        }
        else
        {
            PasswordTxt.ErrText = "";
        }

        if (!isValid)
            return;

        CLoginRequest loginRequest = new CLoginRequest()
        {
            username = UsernameTxt.Text.Trim(),
            password = PasswordTxt.Text.Trim()
        };

        await _viewModel.SignInProcess(loginRequest, FingerPrintCheckBox.IsChecked);
    }

    void FingerprintCheckBox_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        FingerPrintCheckBox.IsChecked = !FingerPrintCheckBox.IsChecked;
    }

    async void FingerPrintBtn_Clicked(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        AuthenticationRequestConfiguration conf =
            new AuthenticationRequestConfiguration(_viewModel.fingerPrintType,
            "Please read your " + _viewModel.fingerPrintType);

        var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
        if (authResult.Authenticated)
        {
            UsernameTxt.Text = SettingsValues.Instance.UserName;
            PasswordTxt.Text = SettingsValues.Instance.Password;
            Login_Clicked(sender, e);
        }
        else
        {
            await App.ShowToast(_viewModel.fingerPrintType + " not match. Try again later.");
        }
    }
}
