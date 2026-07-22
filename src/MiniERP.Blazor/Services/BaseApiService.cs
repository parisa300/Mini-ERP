using System.Net.Http.Json;

namespace MiniERP.Blazor.Services;

public abstract class BaseApiService
{
    protected readonly HttpClient Http;

    protected BaseApiService(HttpClient http)
    {
        Http = http;
    }

    protected async Task<T?> GetAsync<T>(string url)
    {
        return await Http.GetFromJsonAsync<T>(url);
    }

    protected async Task PostAsync<T>(string url, T request)
    {
        var response = await Http.PostAsJsonAsync(url, request);

        response.EnsureSuccessStatusCode();
    }

    protected async Task PutAsync<T>(string url, T request)
    {
        var response = await Http.PutAsJsonAsync(url, request);

        response.EnsureSuccessStatusCode();
    }

    protected async Task DeleteAsync(string url)
    {
        var response = await Http.DeleteAsync(url);

        response.EnsureSuccessStatusCode();
    }
}