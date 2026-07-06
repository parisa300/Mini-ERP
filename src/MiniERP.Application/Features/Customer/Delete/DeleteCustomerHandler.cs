using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Customers.Delete;

public class DeleteCustomerHandler
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x =>
                x.Id == command.Id &&
                x.IsActive,
                cancellationToken);

        if (customer is null)
            throw new Exception("Customer not found.");

        customer.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);
    }
}