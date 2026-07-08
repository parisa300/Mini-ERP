using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Suppliers.GetAll;

public sealed record GetAllSuppliersQuery(
    int Page = 1,
    int PageSize = 10);