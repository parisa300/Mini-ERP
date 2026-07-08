using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Suppliers.Create;

public class CreateSupplierHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationLogger<CreateSupplierHandler> _logger;

    public CreateSupplierHandler(
        IApplicationDbContext context,
        IApplicationLogger<CreateSupplierHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        CreateSupplierCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating supplier {Name}",
            command.Name);

        var exists = await _context.Suppliers.AnyAsync(
            x => x.Name == command.Name,
            cancellationToken);

        if (exists)
            throw new Exception("Supplier already exists.");

        var supplier = new Supplier(
            command.Name,
            command.PhoneNumber,
            command.Email,
            command.Address);

        _context.Suppliers.Add(supplier);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Supplier created {Id}",
            supplier.Id);

        return supplier.Id;
    }
}