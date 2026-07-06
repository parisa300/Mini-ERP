namespace MiniERP.Application.Features.Warehouses.Create;

public sealed record CreateWarehouseCommand(
    string Name,
    string Code,
    string Address,
    string? Description);