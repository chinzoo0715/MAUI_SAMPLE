using System;
namespace maui_template.Constants
{
    public class SettingsValues
    {
        public static SettingsValues Instance { get; }

        static SettingsValues()
        {
            Instance = new SettingsValues();
        }

        #region Setting Constants
        private const string IsFingerPrintEnabledText = "IS_FINGERPRINT_ENABLED";
        private const string UserNameText = "USERNAME";
        private const string PasswordText = "PASSWORD";

        private readonly string IsFingerPrintEnabledDefault = "N";
        private readonly string UsernameDefault = string.Empty;
        private readonly string PasswordDefault = string.Empty;
        #endregion

        #region Settings Properties
        public string IsFingerPrintEnabled
        {
            get => Preferences.Get(IsFingerPrintEnabledText, IsFingerPrintEnabledDefault);
            set => Preferences.Set(IsFingerPrintEnabledText, value);
        }
        public string UserName
        {
            get => Preferences.Get(UserNameText, UsernameDefault);
            set => Preferences.Set(UserNameText, value);
        }
        public string Password
        {
            get => Preferences.Get(PasswordText, PasswordDefault);
            set => Preferences.Set(PasswordText, value);
        }
        #endregion
    }
}

