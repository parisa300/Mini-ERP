using MiniERP.Domain.Common;

namespace MiniERP.Domain.Entities;

public class Warehouse : AuditableEntity
{
    public string Name { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public string? Description { get; private set; }

    private Warehouse()
    {
    }

    public Warehouse(
        string name,
        string code,
        string address,
        string? description)
    {
        Name = name;
        Code = code;
        Address = address;
        Description = description;
    }

    public void Update(
        string name,
        string code,
        string address,
        string? description)
    {
        Name = name;
        Code = code;
        Address = address;
        Description = description;

   SetUpdated();
    }
    public ICollection<Inventory> Inventories { get; private set; }
    = new List<Inventory>();
}