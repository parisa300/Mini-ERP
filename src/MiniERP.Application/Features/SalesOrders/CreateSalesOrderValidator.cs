using FluentValidation;

namespace MiniERP.Application.Features.SalesOrders.Create;

public class CreateSalesOrderValidator
    : AbstractValidator<CreateSalesOrderCommand>
{
    public CreateSalesOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();


        RuleFor(x => x.Items)
            .NotEmpty();
        RuleFor(x => x.WarehouseId)
            .NotEmpty();

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty();

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0);

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThan(0);
            });
    }
}