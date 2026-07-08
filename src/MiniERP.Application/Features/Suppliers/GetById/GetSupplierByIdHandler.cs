using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Suppliers.GetById;

public class GetSupplierByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetSupplierByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierDetailsDto> Handle(
        GetSupplierByIdQuery query,
        CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == query.Id,
                cancellationToken);

        if (supplier is null)
            throw new NotFoundException("Supplier not found.");

        return new SupplierDetailsDto(
            supplier.Id,
            supplier.Name,
            supplier.PhoneNumber,
            supplier.Email,
            supplier.Address);
    }
}