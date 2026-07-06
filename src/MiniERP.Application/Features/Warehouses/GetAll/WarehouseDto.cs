namespace MiniERP.Application.Features.Warehouses.GetAll;

public sealed record WarehouseDto(
    Guid Id,
    string Name,
    string Code,
    string Address,
    string? Description);