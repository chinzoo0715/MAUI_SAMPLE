using System;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_template.Constants;

namespace maui_template.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private Thickness marginTop = new Thickness(0, GlobalValues.MARGIN_TOP, 0, 0);

        [ObservableProperty]
        private Thickness marginBottom = new Thickness(0, 0, 0, GlobalValues.MARGIN_BOTTOM);

        [ObservableProperty]
        private Thickness marginBoth = new Thickness(0, GlobalValues.MARGIN_TOP, 0, GlobalValues.MARGIN_BOTTOM);

        //Can add more variables
    }
}

