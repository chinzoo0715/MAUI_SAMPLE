using maui_template.Views.Auth;

namespace maui_template;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeRouting();
        InitializeComponent();
        SideMenu.SignOutClicked += ShowPromptforSignOut;
    }

    private static void InitializeRouting()
    {
        Routing.RegisterRoute("Login", typeof(LoginPage));
        //Can add more pages in here
    }

    private void ShowPromptforSignOut(object sender, EventArgs e)
    {
        App.MoveToLogin(false);
    }
}

