using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Customers.Create;

public class CreateCustomerHandler
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var customer = new Customer(
            command.FullName,
            command.PhoneNumber,
            command.Email,
            command.Address);

        _context.Customers.Add(customer);

        await _context.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}