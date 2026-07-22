using MiniERP.Blazor.Models;


namespace MiniERP.Blazor.Services;

public class CustomerService : BaseApiService
{
    public CustomerService(HttpClient http)
        : base(http)
    {
    }

    public async Task<PagedResult<CustomerDto>> GetCustomersAsync(
        int page = 1,
        int pageSize = 10,
        string? search = null)
    {
        return await GetAsync<PagedResult<CustomerDto>>
        (
            $"customers?page={page}&pageSize={pageSize}&search={search}"
        )
        ?? new();
    }

    public async Task<CustomerDto?> GetCustomerAsync(Guid id)
    {
        return await GetAsync<CustomerDto>($"customers/{id}");
    }

    public async Task CreateCustomerAsync(CreateCustomerRequest request)
    {
        await PostAsync("customers", request);
    }

    public async Task UpdateCustomerAsync(
        Guid id,
        UpdateCustomerRequest request)
    {
        await PutAsync($"customers/{id}", request);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        await DeleteAsync($"customers/{id}");
    }
}