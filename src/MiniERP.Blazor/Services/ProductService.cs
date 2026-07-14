using System.Net.Http.Json;
using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

public async Task<PagedResult<ProductDto>> GetProductsAsync(
    int page = 1,
    int pageSize = 10,
    string search = "")
{
    return await _http.GetFromJsonAsync<PagedResult<ProductDto>>
    (
        $"products?page={page}&pageSize={pageSize}&search={search}"
    )
    ?? new();
}

public async Task<List<CategoryDto>> GetCategoriesAsync()
{
    return await _http.GetFromJsonAsync<List<CategoryDto>>("categories")
           ?? new();
}
public async Task CreateProductAsync(CreateProductRequest request)
{
    var response = await _http.PostAsJsonAsync("products", request);

    response.EnsureSuccessStatusCode();
}
public async Task<ProductDto?> GetProductAsync(Guid id)
{
    return await _http.GetFromJsonAsync<ProductDto>($"products/{id}");
}

public async Task UpdateProductAsync(Guid id, UpdateProductRequest request)
{
    var response = await _http.PutAsJsonAsync($"products/{id}", request);

    response.EnsureSuccessStatusCode();
}
public async Task DeleteProductAsync(Guid id)
{
    Console.WriteLine($"Deleting => {id}");

    var response = await _http.DeleteAsync($"products/{id}");

    Console.WriteLine(response.StatusCode);

    response.EnsureSuccessStatusCode();
}
}
