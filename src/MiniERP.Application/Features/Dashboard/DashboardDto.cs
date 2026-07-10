namespace MiniERP.Application.Features.Dashboard;

public class DashboardDto
{
    public int TotalProducts { get; set; }

    public int TotalCustomers { get; set; }

    public int TotalSuppliers { get; set; }

    public int TotalWarehouses { get; set; }

    public decimal InventoryValue { get; set; }

    public int PendingPurchaseOrders { get; set; }

    public int PendingSalesOrders { get; set; }

    public decimal TodaySales { get; set; }

    public decimal TodayPurchases { get; set; }
}