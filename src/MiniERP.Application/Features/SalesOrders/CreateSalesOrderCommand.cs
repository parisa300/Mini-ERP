namespace MiniERP.Application.Features.SalesOrders.Create;

public record CreateSalesOrderCommand(
    Guid CustomerId,
    Guid WarehouseId,
    List<CreateSalesOrderItemDto> Items);


public record CreateSalesOrderItemDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);