namespace MiniERP.Application.Features.Inventory.Transfer;

public sealed record TransferInventoryCommand(
    Guid ProductId,
    Guid FromWarehouseId,
    Guid ToWarehouseId,
    int Quantity);