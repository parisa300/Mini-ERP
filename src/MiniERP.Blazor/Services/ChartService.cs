using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class ChartService
{
    public List<ChartPoint> ConvertMonthlySales(
        List<MonthlySalesDto> sales)
    {
        return sales
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .Select(x => new ChartPoint
            {
                Label = $"{x.Year}/{x.Month:00}",
                Value = x.TotalAmount
            })
            .ToList();
    }
}