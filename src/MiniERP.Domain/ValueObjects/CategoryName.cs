namespace MiniERP.Domain.ValueObjects;

public sealed class CategoryName
{
    public string Value { get; }

    private CategoryName(string value)
    {
        Value = value;
    }

    public static CategoryName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Category name is required.");

        if (value.Length > 100)
            throw new ArgumentException("Category name cannot exceed 100 characters.");

        return new CategoryName(value.Trim());
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(CategoryName name)
        => name.Value;
}