using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Customers.GetById;

public class GetCustomerByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetCustomerByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDetailsDto?> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AsNoTracking()
            .Where(x => x.IsActive && x.Id == request.Id)
            .Select(x => new CustomerDetailsDto(
                x.Id,
                x.FullName,
                x.PhoneNumber,
                x.Email,
                x.Address))
            .FirstOrDefaultAsync(cancellationToken);
    }
}