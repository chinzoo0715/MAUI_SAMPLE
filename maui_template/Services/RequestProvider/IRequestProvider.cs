using System;
namespace maui_template.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<T> GetAsync<T>(string uri) where T : class;

        Task<T> PostAsync<T>(string uri, object data = null, string header = "") where T : class;

        Task<T> PutAsync<T>(string uri, object data = null, string header = "") where T : class;

        Task<T> DeleteAsync<T>(string uri) where T : class;

        Task<T> UploadFileAsync<T>(string uri, string fieldname, byte[] image, object data = null) where T : class;
    }
}

