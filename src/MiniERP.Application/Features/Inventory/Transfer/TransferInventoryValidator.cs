using FluentValidation;

namespace MiniERP.Application.Features.Inventory.Transfer;

public class TransferInventoryValidator
    : AbstractValidator<TransferInventoryCommand>
{
    public TransferInventoryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.FromWarehouseId)
            .NotEmpty();

        RuleFor(x => x.ToWarehouseId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x)
            .Must(x => x.FromWarehouseId != x.ToWarehouseId)
            .WithMessage("Source and destination warehouse cannot be the same.");
    }
}