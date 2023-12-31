using maui_template.Constants;
using maui_template.Services.Navigation;
using Newtonsoft.Json;
using RestSharp;

namespace maui_template.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        private readonly INavigationService _navigationService;

        public RequestProvider(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private RestClientOptions _options = new RestClientOptions()
        {
            MaxTimeout = 20000,
            BaseUrl = new Uri(ApiValues.BASE_URL),
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        public async Task<T> GetAsync<T>(string uri) where T : class
        {
            try
            {
                if (!CheckInternet())
                    return null;

                RestClient client = new RestClient(_options);
                RestRequest request = CreateRequest(uri);

                RestResponse response = await client.GetAsync(request).ConfigureAwait(false);
                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<T> PostAsync<T>(string uri, object data = null, string header = "") where T : class
        {
            try
            {
                if (!CheckInternet())
                    return null;

                RestClient client = new RestClient(_options);
                RestRequest request = CreateRequest(uri, data);

                RestResponse response = await client.ExecutePostAsync(request).ConfigureAwait(false);
                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<T> PutAsync<T>(string uri, object data = null, string header = "") where T : class
        {
            try
            {
                if (!CheckInternet())
                    return null;

                RestClient client = new RestClient(_options);
                RestRequest request = CreateRequest(uri, data);

                RestResponse response = await client.PutAsync(request).ConfigureAwait(false);
                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<T> DeleteAsync<T>(string uri) where T : class
        {
            try
            {
                if (!CheckInternet())
                    return null;

                RestClient client = new RestClient(_options);
                RestRequest request = CreateRequest(uri);

                RestResponse response = await client.DeleteAsync(request).ConfigureAwait(false);
                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<T> UploadFileAsync<T>(string uri, string filename, byte[] file, object data = null) where T : class
        {
            try
            {
                if (!CheckInternet())
                    return default(T);

                RestClient client = new RestClient(_options);
                RestRequest request = CreateRequest(uri);

                if (file != null)
                    request.AddFile(filename, file, filename);

                RestResponse response = await client.ExecutePostAsync(request).ConfigureAwait(false);
                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }

        private RestRequest CreateRequest(string uri, object data = null)
        {
            RestRequest request = new RestRequest(uri);
            if (!string.IsNullOrEmpty(GlobalValues.ACCESS_TOKEN))
            {
                App.OnUserInteraction();
                request.AddHeader("Authorization", "Bearer " + GlobalValues.ACCESS_TOKEN);
            }

            if (data != null)
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(data);
            }

            return request;
        }

        private T HandleResponse<T>(RestResponse response) where T : class
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.Created ||
                response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                if (response.Content != "[]")
                {
                    try
                    {
                        var responseObject = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                        return responseObject;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return default(T);
                    }
                }
                else return default(T);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                App.MoveToLogin();
                return default(T);
            }
            else
            {
                return default(T);
            }
        }

        private bool CheckInternet()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            _navigationService.NavigateToAsync("NoInternet");
            return false;
        }
    }
}

