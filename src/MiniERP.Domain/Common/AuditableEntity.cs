namespace MiniERP.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; private set; }

    public void SetUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}