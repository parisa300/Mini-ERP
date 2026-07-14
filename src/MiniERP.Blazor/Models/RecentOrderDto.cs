namespace MiniERP.Blazor.Models;

public class RecentOrderDto
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = "";

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = "";

    public DateTime CreatedAt { get; set; }
}