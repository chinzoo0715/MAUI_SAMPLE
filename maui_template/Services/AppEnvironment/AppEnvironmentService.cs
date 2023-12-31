using System;
using maui_template.Services.Auth;
using maui_template.Services.Home;

namespace maui_template.Services.AppEnvironment
{
    public class AppEnvironmentService : IAppEnvironmentService
    {
        private readonly IAuthService _authService;
        private readonly IHomeService _homeService;

        public AppEnvironmentService
            (IAuthService authService,
            IHomeService homeService)
        {
            _authService = authService;
            _homeService = homeService;
        }

        public IAuthService AuthService { get; private set; }
        public IHomeService HomeService { get; private set; }

        public void UpdateDependencies()
        {
            AuthService = _authService;
            HomeService = _homeService;
        }
    }
}

