using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Inventory.GetTransactions;

public sealed class InventoryTransactionDto
{
    public InventoryTransactionType TransactionType { get; init; }

    public int Quantity { get; init; }

    public string? Description { get; init; }

    public DateTime CreatedAt { get; init; }
}