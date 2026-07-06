namespace MiniERP.Application.Features.Inventory.Receive;

public sealed record ReceiveInventoryCommand(
    Guid ProductId,
    Guid WarehouseId,
    int Quantity);