namespace MiniERP.Blazor.Models;

public class LowStockDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string WarehouseName { get; set; } = string.Empty;

    public decimal Quantity { get; set; }
}