namespace MiniERP.Application.Features.Inventory.Issue;

public sealed record IssueInventoryCommand(
    Guid ProductId,
    Guid WarehouseId,
    int Quantity);