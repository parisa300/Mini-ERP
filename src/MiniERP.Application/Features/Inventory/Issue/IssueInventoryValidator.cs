using FluentValidation;

namespace MiniERP.Application.Features.Inventory.Issue;

public class IssueInventoryValidator
    : AbstractValidator<IssueInventoryCommand>
{
    public IssueInventoryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.WarehouseId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}