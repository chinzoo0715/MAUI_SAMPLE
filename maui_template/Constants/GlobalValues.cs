using System;
using System.Text.RegularExpressions;

namespace maui_template.Constants
{
    public class GlobalValues
    {
        public static string ACCESS_TOKEN = "";

        public static double SESSION_TIMEOUT = 5;

        public const string GOOGLE_MAPS_ANDROID_API_KEY = "YOUR_ANDROID_KEY";

        public const string GOOGLE_MAPS_IOS_API_KEY = "YOU_IOS_KEY";

        public static Regex NUMBER_REGEX = new Regex(@"^[0-9]+$", RegexOptions.Compiled);

        public static Regex PASSWORD_REGEX = new Regex(@"(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8}", RegexOptions.Compiled);

        public static Regex EMAIL_REGEX = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.Compiled);

        public static string[] NON_NOTCH_IPHONE_MODELS = { "iPhone 5", "iPhone 5c", "iPhone 5s", "iPhone 6", "iPhone 6 Plus", "iPhone 6s", "iPhone 6s Plus", "iPhone SE", "iPhone SE (2nd generation)", "iPhone SE (3rd generation)", "iPhone 7", "iPhone 7 Plus", "iPhone 8", "iPhone 8 Plus" };

        public static int MARGIN_TOP;

        public static int MARGIN_BOTTOM;
    }
}

