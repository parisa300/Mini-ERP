namespace MiniERP.Application.Features.Reports.Sales;

public class GetSalesReportQuery
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public Guid? CustomerId { get; set; }
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
    public string? SortBy { get; set; }

    public bool Descending { get; set; } = true;
}