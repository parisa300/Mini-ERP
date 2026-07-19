using System.Net.Http.Json;
using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class CustomerService
{
    private readonly HttpClient _http;

    public CustomerService(HttpClient http)
    {
        _http = http;
    }

    public async Task<PagedResult<CustomerDto>> GetCustomersAsync(
        int page = 1,
        int pageSize = 10,
        string? search = null)
    {
        return await _http.GetFromJsonAsync<PagedResult<CustomerDto>>(
            $"customers?page={page}&pageSize={pageSize}&search={search}")
            ?? new();
    }

    public async Task<CustomerDto?> GetCustomerAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<CustomerDto>(
            $"customers/{id}");
    }

    public async Task CreateCustomerAsync(CreateCustomerRequest request)
    {
        var response = await _http.PostAsJsonAsync("customers", request);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomerAsync(Guid id, UpdateCustomerRequest request)
    {
        var response = await _http.PutAsJsonAsync($"customers/{id}", request);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"customers/{id}");

        response.EnsureSuccessStatusCode();
    }
}