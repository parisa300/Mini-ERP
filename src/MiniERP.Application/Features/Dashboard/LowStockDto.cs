namespace MiniERP.Application.Features.Dashboard;

public class LowStockDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string WarehouseName { get; set; } = string.Empty;
}