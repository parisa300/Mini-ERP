using MiniERP.Domain.Common;
using MiniERP.Domain.Enums;

namespace MiniERP.Domain.Entities;

public class SalesOrder : AuditableEntity
{
    private readonly List<SalesOrderItem> _items = new();

    private SalesOrder()
    {
    }

  public SalesOrder(
    Guid customerId,
    Guid warehouseId)
{
    CustomerId = customerId;
    WarehouseId = warehouseId;
    Status = SalesOrderStatus.Draft;
}

    public Guid CustomerId { get; private set; }

    public SalesOrderStatus Status { get; private set; }
    public Guid WarehouseId { get; private set; }

    public IReadOnlyCollection<SalesOrderItem> Items => _items;

    public decimal TotalAmount =>
        _items.Sum(x => x.Quantity * x.UnitPrice);

    public void AddItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        _items.Add(
            new SalesOrderItem(
                productId,
                quantity,
                unitPrice));
    }

    public void Confirm()
    {
        if (Status != SalesOrderStatus.Draft)
            throw new InvalidOperationException("Order already processed.");

        Status = SalesOrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == SalesOrderStatus.Confirmed)
            throw new InvalidOperationException("Confirmed order cannot be cancelled.");

        Status = SalesOrderStatus.Cancelled;
    }
}