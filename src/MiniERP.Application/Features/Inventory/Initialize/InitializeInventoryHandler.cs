using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;
using InventoryEntity = MiniERP.Domain.Entities.Inventory;

namespace MiniERP.Application.Features.Inventory.Initialize;

public class InitializeInventoryHandler
{
    private readonly IApplicationDbContext _context;

    public InitializeInventoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        InitializeInventoryCommand command,
        CancellationToken cancellationToken)
    {
        // Product must exist
        var productExists = await _context.Products
            .AnyAsync(x => x.Id == command.ProductId, cancellationToken);

        if (!productExists)
           throw new NotFoundException("Product not found.");

        // Warehouse must exist
        var warehouseExists = await _context.Warehouses
            .AnyAsync(x => x.Id == command.WarehouseId, cancellationToken);

        if (!warehouseExists)
         throw new NotFoundException("Warehouse not found.");

        // Inventory must be unique
        var exists = await _context.Inventories
            .AnyAsync(x =>
                x.ProductId == command.ProductId &&
                x.WarehouseId == command.WarehouseId,
                cancellationToken);

        if (exists)
          throw new ConflictException("Inventory already exists.");
        var inventory = new InventoryEntity(
            command.ProductId,
            command.WarehouseId,
            command.Quantity);

        _context.Inventories.Add(inventory);

        await _context.SaveChangesAsync(cancellationToken);

        return inventory.Id;
    }
}