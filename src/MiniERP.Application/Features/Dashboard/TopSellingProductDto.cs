namespace MiniERP.Application.Features.Dashboard;

public class TopSellingProductDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int TotalSold { get; set; }
}