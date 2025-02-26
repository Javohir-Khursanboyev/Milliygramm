using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Milliygramm.Web.Service.Services.Base;

public sealed class ApiService(HttpClient httpClient) : IApiService
{

    public async Task<T1> PostAsync<T1, T2>(string path, T2 postModel)
    {
        var res = await httpClient.PostAsJsonAsync(path, postModel);
        var apiResponse = JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync())
            ?? throw new Exception("Response deserialization failed");

        return apiResponse;
    }

    public Task<T> DeleteAsync<T>(string path)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetFromJsonAsync<T>(string path)
    {
        throw new NotImplementedException();
    }

    public Task<T1> PutAsync<T1, T2>(string path, T2 postModel)
    {
        throw new NotImplementedException();
    }
}