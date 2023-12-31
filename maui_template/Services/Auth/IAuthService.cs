using System;
using maui_template.Models;

namespace maui_template.Services.Auth
{
    public interface IAuthService
    {
        Task<CResponse> SignInProcess(CLoginRequest data);
    }
}

