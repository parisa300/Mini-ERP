using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Customer : AuditableEntity
{
    public string FullName { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    public string Address { get; private set; } = string.Empty;

    public bool IsActive { get; private set; } = true;

    private Customer()
    {
    }

    public Customer(
        string fullName,
        string phoneNumber,
        string? email,
        string address)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
    }

    public void Update(
        string fullName,
        string phoneNumber,
        string? email,
        string address)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;

        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }
}