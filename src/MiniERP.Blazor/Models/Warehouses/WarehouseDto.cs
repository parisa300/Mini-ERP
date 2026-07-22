namespace MiniERP.Blazor.Models;

public class WarehouseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Code { get; set; } = "";

    public string Address { get; set; } = "";

    public string Description { get; set; } = "";
}