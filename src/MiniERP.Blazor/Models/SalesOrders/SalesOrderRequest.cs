namespace MiniERP.Blazor.Models;

public class CreateSalesOrderRequest
{
    public Guid CustomerId { get; set; }

    public Guid WarehouseId { get; set; }

    public List<CreateSalesOrderItemRequest> Items { get; set; } = new();
}