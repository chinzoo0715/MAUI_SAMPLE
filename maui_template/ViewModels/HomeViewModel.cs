using System;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_template.Helpers;
using maui_template.Models;
using maui_template.Services.AppEnvironment;
using Newtonsoft.Json.Linq;

namespace maui_template.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<CCard> custCard;

        [ObservableProperty]
        List<CTransaction> custTransaction;

        private readonly IAppEnvironmentService _appEnvironmentService;

        public HomeViewModel(IAppEnvironmentService appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
        }

        public async Task LoadCustomerData()
        {
            try
            {
                IsBusy = true;

                //Some example you can use

                //CResponse response = await _appEnvironmentService.HomeService.GetCustomerData();
                //if (!await App.CheckResponse(response))
                //{
                //    return;
                //}

                //JObject jsonObject = JObject.Parse((string)response.data);

                //JToken jToken = JsonHelper.RemoveEmptyChildren(jsonObject);

                //if (jToken["cust_info"] != null && !JsonHelper.IsEmpty(jToken["cust_info"]))
                //{
                //    List<CCustomer> customerDatas = jToken["cust_info"].ToObject<List<CCustomer>>();
                //}

                CustCard = new List<CCard>()
                {
                    new CCard(){
                        CARD_NAME= "Alex Josh",
                        CARD_NUMBER = "**** **** **** **96",
                        CARD_BALANCE = 567908.88,
                        CARD_TYPE = "Master"
                    },
                    new CCard(){
                        CARD_NAME= "Alex Josh",
                        CARD_NUMBER = "**** **** **** **67",
                        CARD_BALANCE = 1467408.88,
                        CARD_TYPE = "Visa"
                    },
                    new CCard(){
                        CARD_NAME= "Alex Josh",
                        CARD_NUMBER = "**** **** **** **23",
                        CARD_BALANCE = 2167908.88,
                        CARD_TYPE = "Visa"
                    },
                    new CCard(){
                        CARD_NAME= "Alex Josh",
                        CARD_NUMBER = "**** **** **** **43",
                        CARD_BALANCE = 27908.88,
                        CARD_TYPE = "Master"
                    }
                };

                CustTransaction = new List<CTransaction>()
                {
                    new CTransaction()
                    {
                        NAME = "DSTV",
                        DESCRIPTION="August Subscription",
                        AMT=-35.50
                    },
                    new CTransaction()
                    {
                        NAME = "Interest",
                        DESCRIPTION="Interest on account balance",
                        AMT=1.45
                    },
                    new CTransaction()
                    {
                        NAME = "Netflix",
                        DESCRIPTION="August Subscription",
                        AMT=12
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

