namespace maui_template.Views.Controller;

public partial class InputView : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InputView), string.Empty, BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPasswordValue), typeof(bool), typeof(InputView), false);

    public bool IsPasswordValue
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolderTxt), typeof(string), typeof(InputView), string.Empty);

    public string PlaceHolderTxt
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }

    public static readonly BindableProperty KeyBoardProperty = BindableProperty.Create(nameof(KeyBoardType), typeof(Keyboard), typeof(InputView), Keyboard.Text, BindingMode.TwoWay);

    public Keyboard KeyBoardType
    {
        get { return (Keyboard)GetValue(KeyBoardProperty); }
        set { SetValue(KeyBoardProperty, value); }
    }

    public static readonly BindableProperty ErrTextProperty = BindableProperty.Create(nameof(ErrText), typeof(string), typeof(InputView), string.Empty, BindingMode.TwoWay, propertyChanged: OnErrTextChanged);

    public string ErrText
    {
        get => (string)GetValue(ErrTextProperty);
        set => SetValue(ErrTextProperty, value);
    }

    private static void OnErrTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        InputView inputView = bindable as InputView;
        inputView.UpdateXaml(newValue as string);
    }

    private void UpdateXaml(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            ErrorTxt.Opacity = 0;
        else
        {
            ErrorTxt.Text = newValue;
            ErrorTxt.Opacity = 1;
        }
    }

    public static readonly BindableProperty IsBlockProperty = BindableProperty.Create(
            nameof(IsBlock),
            typeof(bool),
            typeof(InputView),
            false,
            BindingMode.OneWay,
            propertyChanged: OnIsBlockPropertyChanged);

    public bool IsBlock
    {
        get => (bool)GetValue(IsBlockProperty);
        set
        {
            if (value != IsBlock)
            {
                SetValue(IsBlockProperty, value);
            }
        }
    }

    private static void OnIsBlockPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputView buttonView && newValue is bool isBlock)
        {
            buttonView.MainEntry.IsReadOnly = isBlock;
        }
    }

    public bool IsBusy
    {
        get { return IsBlock; }
    }

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLengthNumber), typeof(int), typeof(InputView), -1);

    public int MaxLengthNumber
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public InputView()
    {
        BindingContext = this;
        InitializeComponent();
    }

    void MainEntry_Focused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        EntryBorder.Stroke = Color.FromArgb("#4A3FFF");
    }

    void MainEntry_Unfocused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        EntryBorder.Stroke = Color.FromArgb("#B2B2B2");
    }

    void ShowPass(System.Object sender, TappedEventArgs e)
    {
        MainEntry.IsPassword = !MainEntry.IsPassword;
    }

    public event EventHandler InputTextChanged;

    void EntryTxt_Changed(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        InputTextChanged?.Invoke(this, e);
    }
}
