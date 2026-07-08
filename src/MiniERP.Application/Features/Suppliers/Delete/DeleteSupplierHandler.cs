using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Suppliers.Delete;

public class DeleteSupplierHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationLogger<DeleteSupplierHandler> _logger;

    public DeleteSupplierHandler(
        IApplicationDbContext context,
        IApplicationLogger<DeleteSupplierHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(
        DeleteSupplierCommand command,
        CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(
                x => x.Id == command.Id,
                cancellationToken);

        if (supplier is null)
            throw new NotFoundException("Supplier not found.");

        _context.Suppliers.Remove(supplier);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Supplier deleted {Id}",
            supplier.Id);
    }
}