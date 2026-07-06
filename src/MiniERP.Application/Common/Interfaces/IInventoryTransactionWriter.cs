using MiniERP.Domain.Entities;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Common.Interfaces;

public interface IInventoryTransactionWriter
{
    Task WriteAsync(
        Inventory inventory,
        int quantity,
        InventoryTransactionType type,
        string? referenceNumber,
        string? description,
        CancellationToken cancellationToken);
}