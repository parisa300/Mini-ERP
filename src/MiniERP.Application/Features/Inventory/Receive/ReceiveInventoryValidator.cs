using FluentValidation;

namespace MiniERP.Application.Features.Inventory.Receive;

public class ReceiveInventoryValidator
    : AbstractValidator<ReceiveInventoryCommand>
{
    public ReceiveInventoryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.WarehouseId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}