namespace maui_template.Views.Controller;

public partial class SideMenuView : ContentView
{
    public event EventHandler SignOutClicked;

    public SideMenuView()
    {
        InitializeComponent();
        VersionLbl.Text = AppInfo.VersionString;
    }

    void SignOut_Clicked(System.Object sender, System.EventArgs e)
    {
        SignOutClicked.Invoke(this, e);
    }
}
