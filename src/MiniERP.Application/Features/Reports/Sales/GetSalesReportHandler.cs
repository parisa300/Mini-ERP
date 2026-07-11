using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Models;

namespace MiniERP.Application.Features.Reports.Sales;

public class GetSalesReportHandler
{
    private readonly IApplicationDbContext _context;

    public GetSalesReportHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<SalesReportDto>> Handle(
        GetSalesReportQuery query,
        CancellationToken cancellationToken)
    {
        var salesQuery = _context.SalesOrders
            .AsQueryable();

        if (query.FromDate.HasValue)
        {
            salesQuery = salesQuery.Where(x =>
                x.CreatedAt >= query.FromDate.Value);
        }

        if (query.ToDate.HasValue)
        {
            salesQuery = salesQuery.Where(x =>
                x.CreatedAt <= query.ToDate.Value);
        }

        if (query.CustomerId.HasValue)
        {
            salesQuery = salesQuery.Where(x =>
                x.CustomerId == query.CustomerId.Value);
        }
var totalCount = await salesQuery.CountAsync(cancellationToken);
     var reportQuery = salesQuery
    .Include(x => x.Customer)
    .Include(x => x.Items)
    .Select(x => new SalesReportDto
    {
        SalesOrderId = x.Id,
        Date = x.CreatedAt,
        CustomerName = x.Customer.FullName,
        TotalAmount = x.Items.Sum(i => i.Quantity * i.UnitPrice),
        ItemsCount = x.Items.Count,
        Status = x.Status.ToString()
    });

switch (query.SortBy?.ToLower())
{
    case "customer":
        reportQuery = query.Descending
            ? reportQuery.OrderByDescending(x => x.CustomerName)
            : reportQuery.OrderBy(x => x.CustomerName);
        break;

    case "amount":
        reportQuery = query.Descending
            ? reportQuery.OrderByDescending(x => x.TotalAmount)
            : reportQuery.OrderBy(x => x.TotalAmount);
        break;

    default:
        reportQuery = query.Descending
            ? reportQuery.OrderByDescending(x => x.Date)
            : reportQuery.OrderBy(x => x.Date);
        break;
}

var result = await reportQuery
    .Skip((query.PageNumber - 1) * query.PageSize)
    .Take(query.PageSize)
    .ToListAsync(cancellationToken);

     return new PagedResult<SalesReportDto>
{
    Items = result,
    PageNumber = query.PageNumber,
    PageSize = query.PageSize,
    TotalCount = totalCount
};
    }
}