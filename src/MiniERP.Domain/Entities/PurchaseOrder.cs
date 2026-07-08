using MiniERP.Domain.Common;
using MiniERP.Domain.Enums;

namespace MiniERP.Domain.Entities;

public class PurchaseOrder : AuditableEntity
{
    public Guid SupplierId { get; private set; }

    public Guid WarehouseId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public PurchaseOrderStatus Status { get; private set; }

    public Supplier Supplier { get; private set; } = null!;

    public Warehouse Warehouse { get; private set; } = null!;

    public ICollection<PurchaseOrderItem> Items { get; private set; }
        = new List<PurchaseOrderItem>();

    public decimal TotalAmount =>
        Items.Sum(x => x.TotalPrice);

    private PurchaseOrder()
    {
    }

    public PurchaseOrder(
        Guid supplierId,
        Guid warehouseId)
    {
        SupplierId = supplierId;
        WarehouseId = warehouseId;
        OrderDate = DateTime.UtcNow;
        Status = PurchaseOrderStatus.Draft;
    }

    public void AddItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new Exception("Order cannot be modified.");

        Items.Add(new PurchaseOrderItem(
            productId,
            quantity,
            unitPrice));

        SetUpdated();
    }

    public void Submit()
    {
        if (!Items.Any())
            throw new Exception("Order has no items.");

        if (Status != PurchaseOrderStatus.Draft)
            throw new Exception("Invalid status.");

        Status = PurchaseOrderStatus.Submitted;

        SetUpdated();
    }

    public void Receive()
    {
        if (Status != PurchaseOrderStatus.Submitted)
            throw new Exception("Only submitted orders can be received.");

        Status = PurchaseOrderStatus.Received;

        SetUpdated();
    }

    public void Cancel()
    {
        if (Status == PurchaseOrderStatus.Received)
            throw new Exception("Received order cannot be cancelled.");

        Status = PurchaseOrderStatus.Cancelled;

        SetUpdated();
    }
}