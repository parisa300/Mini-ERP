using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;

    private Category()
    {
    }

  public Category(string name, string? description)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Category name is required.", nameof(name));

    if (name.Length > 100)
        throw new ArgumentException("Category name cannot exceed 100 characters.", nameof(name));

    if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
        throw new ArgumentException("Description cannot exceed 500 characters.", nameof(description));

    Name = name.Trim();
    Description = description?.Trim();
}

  public void Update(string name, string? description)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Category name is required.", nameof(name));

    if (name.Length > 100)
        throw new ArgumentException("Category name cannot exceed 100 characters.", nameof(name));

    if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
        throw new ArgumentException("Description cannot exceed 500 characters.", nameof(description));

    Name = name.Trim();
    Description = description?.Trim();

    SetUpdated();
}

    public void Deactivate()
    {
        IsActive = false;

        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;

        SetUpdated();
    }
    public ICollection<Product> Products { get; private set; } = new List<Product>();
}