using FluentValidation;

namespace MiniERP.Application.Features.Inventory.Initialize;

public class InitializeInventoryValidator
    : AbstractValidator<InitializeInventoryCommand>
{
    public InitializeInventoryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.WarehouseId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);
    }
}