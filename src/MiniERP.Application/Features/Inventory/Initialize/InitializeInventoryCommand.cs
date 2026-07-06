namespace MiniERP.Application.Features.Inventory.Initialize;

public sealed record InitializeInventoryCommand(
    Guid ProductId,
    Guid WarehouseId,
    int Quantity);