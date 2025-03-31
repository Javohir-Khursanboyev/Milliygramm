namespace Milliygramm.Web.Services.Base;

public interface IApiService
{
    Task<T> GetFromJsonAsync<T>(string path);
    Task<T1> PostAsync<T1, T2>(string path, T2 postModel);
    Task<T1> PutAsync<T1, T2>(string path, T2 postModel);
    Task<T> DeleteAsync<T>(string path);
    Task<T> PostMultipartFormDataAsync<T>(string path, MultipartFormDataContent content);
}
