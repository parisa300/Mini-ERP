namespace MiniERP.Blazor.Models;

public class CreateSalesOrderItemRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}