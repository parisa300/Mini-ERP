namespace MiniERP.Application.Features.Warehouses.Update;

public sealed record UpdateWarehouseCommand(
    Guid Id,
    string Name,
    string Code,
    string Address,
    string? Description);