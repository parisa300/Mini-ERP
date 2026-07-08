using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class PurchaseOrderItem : AuditableEntity
{
    public Guid PurchaseOrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public PurchaseOrder PurchaseOrder { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    private PurchaseOrderItem()
    {
    }

    public PurchaseOrderItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        if (unitPrice <= 0)
            throw new Exception("Unit price must be greater than zero.");

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void Update(
        int quantity,
        decimal unitPrice)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        if (unitPrice <= 0)
            throw new Exception("Unit price must be greater than zero.");

        Quantity = quantity;
        UnitPrice = unitPrice;

        SetUpdated();
    }
}