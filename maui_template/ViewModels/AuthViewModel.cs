using System;
using maui_template.Constants;
using maui_template.Models;
using maui_template.Services.AppEnvironment;
using maui_template.Services.Navigation;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace maui_template.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAppEnvironmentService _appEnvironmentService;
        private readonly INavigationService _navigationService;

        public string fingerPrintType;

        public AuthViewModel(IAppEnvironmentService appEnvironmentService, INavigationService navigationService)
        {
            _appEnvironmentService = appEnvironmentService;
            _navigationService = navigationService;
        }

        public async Task SignInProcess(CLoginRequest data, bool isFingerPrintChecked)
        {
            try
            {
                IsBusy = true;

                //You can use http request when your service ready 

                //CResponse response = await _appEnvironmentService.AuthService.SignInProcess(data);
                //if (!await App.CheckResponse(response, false))
                //{
                //    return;
                //}

                SettingsValues.Instance.UserName = data.username;
                SettingsValues.Instance.Password = data.password;

                if (isFingerPrintChecked)
                {
                    AuthenticationRequestConfiguration conf =
                                new AuthenticationRequestConfiguration(fingerPrintType,
                                "Please read your " + fingerPrintType);

                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
                    if (authResult.Authenticated)
                    {
                        SettingsValues.Instance.IsFingerPrintEnabled = "Y";
                    }
                    else
                        return;
                }

                App.StartSession();
                await _navigationService.NavigateToAsync("//Main/Home");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                await App.ShowToast("There is an error. Try again later");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

