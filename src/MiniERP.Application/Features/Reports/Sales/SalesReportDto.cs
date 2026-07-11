namespace MiniERP.Application.Features.Reports.Sales;

public class SalesReportDto
{
    public Guid SalesOrderId { get; set; }

    public DateTime Date { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public int ItemsCount { get; set; }

    public string Status { get; set; } = string.Empty;
}