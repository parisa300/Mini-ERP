using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class RefreshToken : AuditableEntity
{
    public Guid UserId { get; private set; }

    public string Token { get; private set; } = null!;

    public DateTime ExpiresAt { get; private set; }

    public bool IsRevoked { get; private set; }

    public User User { get; private set; } = null!;

    private RefreshToken()
    {
    }

    public RefreshToken(
        Guid userId,
        string token,
        DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }

    public bool IsExpired =>
        DateTime.UtcNow >= ExpiresAt;

    public void Revoke()
    {
        IsRevoked = true;

        SetUpdated();
    }
}