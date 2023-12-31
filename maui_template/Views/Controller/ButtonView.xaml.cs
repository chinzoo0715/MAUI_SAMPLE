namespace maui_template.Views.Controller;

public partial class ButtonView : ContentView
{
    //Properties
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(ButtonView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextPropertyChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var buttonView = (ButtonView)bindable;
        buttonView.ButtonTxt.Text = newValue.ToString();
    }

    public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(
            nameof(ButtonColor),
            typeof(Color),
            typeof(ButtonView),
            Color.FromArgb("#4A3FFF"),
            BindingMode.TwoWay);

    public Color ButtonColor
    {
        get => (Color)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);
    }

    public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create(
            nameof(ButtonHeight),
            typeof(int),
            typeof(ButtonView),
            70,
            BindingMode.TwoWay);

    public int ButtonHeight
    {
        get => (int)GetValue(ButtonHeightProperty);
        set => SetValue(ButtonHeightProperty, value);
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(ButtonView),
            Colors.White,
            BindingMode.TwoWay);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(ButtonView),
            Colors.Transparent,
            BindingMode.TwoWay);

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly BindableProperty IsBlockProperty = BindableProperty.Create(
        nameof(IsBlock),
        typeof(bool),
        typeof(ButtonView),
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

    //Block change event 
    private static void OnIsBlockPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ButtonView buttonView && newValue is bool isBlock)
        {
            buttonView.MainButton.IsEnabled = !isBlock;
            buttonView.LoadingFrame.IsVisible = isBlock;
        }
    }

    public static readonly BindableProperty IsEnableProperty = BindableProperty.Create(
        nameof(IsEnable),
        typeof(bool),
        typeof(ButtonView),
        true,
        BindingMode.OneWay,
        propertyChanged: OnIsEnablePropertyChanged);

    public bool IsEnable
    {
        get => (bool)GetValue(IsEnableProperty);
        set
        {
            if (value != IsEnable)
            {
                SetValue(IsEnableProperty, value);
            }
        }
    }

    //Enable change event
    private static void OnIsEnablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ButtonView buttonView && newValue is bool isEnable)
        {
            buttonView.MainButton.IsEnabled = isEnable;
        }
    }

    public bool IsBusy
    {
        get { return IsBlock; }
    }

    public event EventHandler Clicked;

    public ButtonView()
    {
        BindingContext = this;
        InitializeComponent();
    }

    private void HandleButtonClicked(object sender, TappedEventArgs e)
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }
}
