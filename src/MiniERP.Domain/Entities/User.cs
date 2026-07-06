using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Role { get; private set; } = "Employee";

    public bool IsActive { get; private set; } = true;

    private User()
    {
    }

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
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
}