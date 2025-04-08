using Milliygramm.Model.ApiModels;

namespace Milliygramm.Web.Services.Base;

public interface IApiService
{
    Task<Response> PostAsync<T>(string path, T postModel);
    Task<Response> DeleteAsync(string path);
    Task<Response> GetAsync(string path);
    Task<Response> PutAsync<T>(string path, T postModel);
    Task<Response> PostMultipartFormDataAsync(string path, MultipartFormDataContent content);
}
