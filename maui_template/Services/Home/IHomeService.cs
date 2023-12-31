using System;
using maui_template.Models;

namespace maui_template.Services.Home
{
    public interface IHomeService
    {
        Task<CResponse> GetCustomerData();
    }
}

