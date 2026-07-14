using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Dashboard;

public class GetDashboardHandler
{
    private readonly IApplicationDbContext _context;

    public GetDashboardHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> Handle(
        GetDashboardQuery query,
        CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var lowStockProducts =
    await _context.Inventories
        .Where(x => x.Quantity <= 5)
        .Select(x => new LowStockProductDto
        {
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            Quantity = x.Quantity
        })
        .ToListAsync(cancellationToken);

        var topSellingProducts =
    await _context.SalesOrderItems
        .Where(x => x.SalesOrder.Status == SalesOrderStatus.Confirmed)
        .GroupBy(x => new
        {
            x.ProductId,
            x.Product.Name
        })
        .Select(g => new TopSellingProductDto
        {
            ProductId = g.Key.ProductId,
            ProductName = g.Key.Name,
            TotalSold = g.Sum(x => x.Quantity)
        })
        .OrderByDescending(x => x.TotalSold)
        .Take(5)
        .ToListAsync(cancellationToken);
var monthlySales =
    await _context.SalesOrderItems
        .Where(x => x.SalesOrder.Status == SalesOrderStatus.Confirmed)
        .GroupBy(x => new
        {
            x.SalesOrder.CreatedAt.Year,
            x.SalesOrder.CreatedAt.Month
        })
        .Select(g => new MonthlySalesDto
        {
            Year = g.Key.Year,
            Month = g.Key.Month,

            TotalAmount = g.Sum(x =>
                x.Quantity * x.UnitPrice),

            OrdersCount = g
                .Select(x => x.SalesOrderId)
                .Distinct()
                .Count()
        })
        .OrderBy(x => x.Month)
        .ToListAsync(cancellationToken);

        var lowStockAlerts =
    await _context.Inventories
        .Where(x => x.Quantity <= 5)
        .Select(x => new LowStockDto
        {
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            WarehouseName = x.Warehouse.Name,
            Quantity = x.Quantity
        })
        .OrderBy(x => x.Quantity)
        .ToListAsync(cancellationToken);

var recentOrders =
    await _context.SalesOrders
        .OrderByDescending(x => x.CreatedAt)
        .Take(5)
        .Select(x => new RecentOrderDto
        {
            Id = x.Id,
            CustomerName = x.Customer.FullName,
            TotalAmount = x.Items.Sum(i => i.Quantity * i.UnitPrice),
            Status = x.Status.ToString(),
            CreatedAt = x.CreatedAt
        })
        .ToListAsync(cancellationToken);
      
        return new DashboardDto
        {
            TotalProducts =
                await _context.Products.CountAsync(cancellationToken),

            TotalCustomers =
                await _context.Customers.CountAsync(cancellationToken),

            TotalSuppliers =
                await _context.Suppliers.CountAsync(cancellationToken),

            TotalWarehouses =
                await _context.Warehouses.CountAsync(cancellationToken),

            InventoryValue =
                await _context.Inventories
                    .SumAsync(
                        x => x.Quantity * x.Product.Price,
                        cancellationToken),

            PendingPurchaseOrders =
                await _context.PurchaseOrders
                    .CountAsync(
                        x => x.Status == PurchaseOrderStatus.Submitted,
                        cancellationToken),

            PendingSalesOrders =
                await _context.SalesOrders
                    .CountAsync(
                        x => x.Status == SalesOrderStatus.Draft,
                        cancellationToken),

        TodaySales =
    await _context.SalesOrderItems
        .Where(x =>
            x.SalesOrder.CreatedAt.Date == today &&
            x.SalesOrder.Status == SalesOrderStatus.Confirmed)
        .SumAsync(
            x => x.Quantity * x.UnitPrice,
            cancellationToken),

          TodayPurchases =
    await _context.PurchaseOrderItems
        .Where(x =>
            x.PurchaseOrder.CreatedAt.Date == today &&
            x.PurchaseOrder.Status == PurchaseOrderStatus.Received)
        .SumAsync(
            x => x.Quantity * x.UnitPrice,
            cancellationToken),

            LowStockProducts = lowStockProducts,
            TopSellingProducts = topSellingProducts,
            MonthlySales = monthlySales,
            LowStockAlerts = lowStockAlerts,
            RecentOrders = recentOrders,
        };
    }
}