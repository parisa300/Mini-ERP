using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Suppliers.Update;

public class UpdateSupplierHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationLogger<UpdateSupplierHandler> _logger;

    public UpdateSupplierHandler(
        IApplicationDbContext context,
        IApplicationLogger<UpdateSupplierHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(
        UpdateSupplierCommand command,
        CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(
                x => x.Id == command.Id,
                cancellationToken);

        if (supplier is null)
            throw new NotFoundException("Supplier not found.");

        supplier.Update(
            command.Name,
            command.PhoneNumber,
            command.Email,
            command.Address);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Supplier updated {Id}",
            supplier.Id);
    }
}