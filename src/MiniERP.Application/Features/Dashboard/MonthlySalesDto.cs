namespace MiniERP.Application.Features.Dashboard;

public class MonthlySalesDto
{
    public int Year { get; set; }

    public int Month { get; set; }

    public decimal TotalAmount { get; set; }

    public int OrdersCount { get; set; }
}