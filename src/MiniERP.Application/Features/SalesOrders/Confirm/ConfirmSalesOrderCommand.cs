namespace MiniERP.Application.Features.SalesOrders.Confirm;

public record ConfirmSalesOrderCommand(
    Guid SalesOrderId);