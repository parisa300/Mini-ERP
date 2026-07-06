using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Customers.Update;

public class UpdateCustomerHandler
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x =>
                x.Id == command.Id &&
                x.IsActive,
                cancellationToken);

        if (customer is null)
            throw new Exception("Customer not found.");

        customer.Update(
            command.FullName,
            command.PhoneNumber,
            command.Email,
            command.Address);

        await _context.SaveChangesAsync(cancellationToken);
    }
}