using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Supplier : AuditableEntity
{
    public string Name { get; private set; } = null!;

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }

    public string? Address { get; private set; }

    private Supplier()
    {
    }

    public Supplier(
        string name,
        string? phoneNumber,
        string? email,
        string? address)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
    }

    public void Update(
        string name,
        string? phoneNumber,
        string? email,
        string? address)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;

        SetUpdated();
    }
}