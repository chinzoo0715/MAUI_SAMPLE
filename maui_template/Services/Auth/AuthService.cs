using System;
using maui_template.Constants;
using maui_template.Models;
using maui_template.Services.RequestProvider;

namespace maui_template.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRequestProvider _requestProvider;

        public AuthService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<CResponse> SignInProcess(CLoginRequest data)
        {
            string uri = ApiValues.LOGIN_URL;
            CResponse result = await _requestProvider.PostAsync<CResponse>(uri, data).ConfigureAwait(false);
            return result;
        }
    }
}

