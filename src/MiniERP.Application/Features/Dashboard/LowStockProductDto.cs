namespace MiniERP.Application.Features.Dashboard;

public class LowStockProductDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }
}