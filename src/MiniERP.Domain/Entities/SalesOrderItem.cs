using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class SalesOrderItem : BaseEntity
{
    private SalesOrderItem()
    {
    }

    public SalesOrderItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid SalesOrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }


    public SalesOrder SalesOrder { get; private set; } = null!;
}