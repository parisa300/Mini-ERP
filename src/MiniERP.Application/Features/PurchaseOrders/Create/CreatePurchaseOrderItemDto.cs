namespace MiniERP.Application.Features.PurchaseOrders.Create;

public sealed record CreatePurchaseOrderItemDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);