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
            cancellationToken)
        };
    }
}