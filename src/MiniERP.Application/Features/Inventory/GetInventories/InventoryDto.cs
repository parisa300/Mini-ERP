namespace MiniERP.Application.Features.Inventory.GetInventories;

public sealed class InventoryDto
{
    public Guid InventoryId { get; init; }

    public Guid ProductId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public Guid WarehouseId { get; init; }

    public string WarehouseName { get; init; } = string.Empty;

    public int Quantity { get; init; }
}