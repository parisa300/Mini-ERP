namespace MiniERP.Application.Features.Products.GetAll;

public class ProductFilter
{
    public string? Search { get; init; }

    public Guid? CategoryId { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public string? SortBy { get; init; }
}