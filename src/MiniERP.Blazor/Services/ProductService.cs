using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class ProductService : BaseApiService
{
    public ProductService(HttpClient http)
        : base(http)
    {
    }

    public async Task<PagedResult<ProductDto>> GetProductsAsync(
        int page = 1,
        int pageSize = 10,
        string search = "")
    {
        return await GetAsync<PagedResult<ProductDto>>
        (
            $"products?page={page}&pageSize={pageSize}&search={search}"
        )
        ?? new();
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        return await GetAsync<List<CategoryDto>>("categories")
               ?? new();
    }

    public async Task CreateProductAsync(CreateProductRequest request)
    {
        await PostAsync("products", request);
    }

    public async Task<ProductDto?> GetProductAsync(Guid id)
    {
        return await GetAsync<ProductDto>($"products/{id}");
    }

    public async Task UpdateProductAsync(
        Guid id,
        UpdateProductRequest request)
    {
        await PutAsync($"products/{id}", request);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await DeleteAsync($"products/{id}");
    }
}