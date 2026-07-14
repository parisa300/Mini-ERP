namespace MiniERP.Blazor.Models;

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

    public List<object> LowStockProducts { get; set; } = new();

 //   public List<object> TopSellingProducts { get; set; } = new();

    public List<MonthlySalesDto> MonthlySales { get; set; } = new();
    

 //   public List<object> LowStockAlerts { get; set; } = new();
    public List<TopSellingProductDto> TopSellingProducts { get; set; } = [];
    public List<LowStockDto> LowStockAlerts { get; set; } = [];
    public List<RecentOrderDto> RecentOrders { get; set; } = [];
}