using System;
using maui_template.Services.Auth;
using maui_template.Services.Home;

namespace maui_template.Services.AppEnvironment
{
    public interface IAppEnvironmentService
    {
        IAuthService AuthService { get; }
        IHomeService HomeService { get; }

        void UpdateDependencies();
    }
}

