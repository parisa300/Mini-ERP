namespace MiniERP.Application.Features.PurchaseOrders.Create;

public sealed record CreatePurchaseOrderCommand(
    Guid SupplierId,
    Guid WarehouseId,
    List<CreatePurchaseOrderItemDto> Items);