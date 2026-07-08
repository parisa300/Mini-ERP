using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using InventoryEntity = MiniERP.Domain.Entities.Inventory;

namespace MiniERP.Application.Features.Inventory.Initialize;

public class InitializeInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationLogger<InitializeInventoryHandler> _logger;

    public InitializeInventoryHandler(
        IApplicationDbContext context,
        IApplicationLogger<InitializeInventoryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        InitializeInventoryCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Inventory initialization started. Product:{ProductId}, Warehouse:{WarehouseId}, Quantity:{Quantity}",
            command.ProductId,
            command.WarehouseId,
            command.Quantity);

        var productExists = await _context.Products
            .AnyAsync(
                x => x.Id == command.ProductId,
                cancellationToken);

        if (!productExists)
        {
            _logger.LogWarning(
                "Product not found. Product:{ProductId}",
                command.ProductId);

            throw new NotFoundException("Product not found.");
        }

        var warehouseExists = await _context.Warehouses
            .AnyAsync(
                x => x.Id == command.WarehouseId,
                cancellationToken);

        if (!warehouseExists)
        {
            _logger.LogWarning(
                "Warehouse not found. Warehouse:{WarehouseId}",
                command.WarehouseId);

            throw new NotFoundException("Warehouse not found.");
        }

        var exists = await _context.Inventories
            .AnyAsync(
                x => x.ProductId == command.ProductId &&
                     x.WarehouseId == command.WarehouseId,
                cancellationToken);

        if (exists)
        {
            _logger.LogWarning(
                "Inventory already exists. Product:{ProductId}, Warehouse:{WarehouseId}",
                command.ProductId,
                command.WarehouseId);

            throw new ConflictException("Inventory already exists.");
        }

        var inventory = new InventoryEntity(
            command.ProductId,
            command.WarehouseId,
            command.Quantity);

        _context.Inventories.Add(inventory);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Inventory initialized successfully. Inventory:{InventoryId}",
            inventory.Id);

        return inventory.Id;
    }
}