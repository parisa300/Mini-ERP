using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Inventory : AuditableEntity
{
    public Guid ProductId { get; private set; }

    public Guid WarehouseId { get; private set; }

    public int Quantity { get; private set; }

    public Product Product { get; private set; } = null!;

    public Warehouse Warehouse { get; private set; } = null!;

    private Inventory()
    {
    }

    public Inventory(
        Guid productId,
        Guid warehouseId,
        int quantity)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        Quantity = quantity;
    }

    public void Increase(int quantity)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        Quantity += quantity;

        SetUpdated();
    }

    public void Decrease(int quantity)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        if (Quantity < quantity)
            throw new Exception("Insufficient inventory.");

        Quantity -= quantity;

        SetUpdated();
    }
    public ICollection<InventoryTransaction> Transactions
    { get; private set; } = new List<InventoryTransaction>();
}