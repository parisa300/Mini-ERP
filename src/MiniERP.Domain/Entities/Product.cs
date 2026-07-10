using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    public bool IsActive { get; private set; } = true;

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = default!;

    private Product()
    {
    }

    public Product(
        string name,
        string? description,
        decimal price,
        int stock,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }

    public void Update(
        string name,
        string? description,
        decimal price,
        int stock,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;

        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }
    public ICollection<Inventory> Inventories { get; private set; }
    = new List<Inventory>();
    public ICollection<SalesOrderItem> SalesOrderItems { 
        get; private set; } = new List<SalesOrderItem>();
}