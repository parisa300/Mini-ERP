using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Products.GetAll;

public sealed record GetAllProductsQuery(
    PaginationRequest Pagination,
    ProductFilter Filter);