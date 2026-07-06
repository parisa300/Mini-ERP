namespace MiniERP.Application.Features.Categories.GetAll;

public sealed record GetAllCategoriesQuery(
    int Page = 1,
    int PageSize = 10);