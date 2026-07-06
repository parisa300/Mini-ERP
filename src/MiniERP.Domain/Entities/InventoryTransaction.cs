using MiniERP.Domain.Common;
using MiniERP.Domain.Enums;

namespace MiniERP.Domain.Entities;

public class InventoryTransaction : AuditableEntity
{
    public Guid InventoryId { get; private set; }

    public int Quantity { get; private set; }

    public InventoryTransactionType TransactionType { get; private set; }

    public string? ReferenceNumber { get; private set; }

    public string? Description { get; private set; }

    public Inventory Inventory { get; private set; } = null!;

    private InventoryTransaction()
    {
    }

    public InventoryTransaction(
        Guid inventoryId,
        int quantity,
        InventoryTransactionType transactionType,
        string? referenceNumber,
        string? description)
    {
        InventoryId = inventoryId;
        Quantity = quantity;
        TransactionType = transactionType;
        ReferenceNumber = referenceNumber;
        Description = description;
    }
}