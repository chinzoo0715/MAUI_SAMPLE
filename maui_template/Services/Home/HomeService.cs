using System;
using maui_template.Constants;
using maui_template.Models;
using maui_template.Services.RequestProvider;

namespace maui_template.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly IRequestProvider _requestProvider;

        public HomeService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<CResponse> GetCustomerData()
        {
            var uri = ApiValues.GET_CUSTOMER_INFO_URL;
            CResponse result = await _requestProvider.GetAsync<CResponse>(uri).ConfigureAwait(false);
            return result;
        }
    }
}

