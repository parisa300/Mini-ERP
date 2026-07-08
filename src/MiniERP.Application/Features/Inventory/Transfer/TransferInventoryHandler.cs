using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;
using InventoryEntity = MiniERP.Domain.Entities.Inventory;

namespace MiniERP.Application.Features.Inventory.Transfer;

public class TransferInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;
    private readonly IApplicationLogger<TransferInventoryHandler> _logger;

    public TransferInventoryHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter,
        IApplicationLogger<TransferInventoryHandler> logger)
    {
        _context = context;
        _transactionWriter = transactionWriter;
        _logger = logger;
    }

    public async Task Handle(
        TransferInventoryCommand command,
        CancellationToken cancellationToken)
    {
        if (command.FromWarehouseId == command.ToWarehouseId)
            throw new ValidationException(
                "Source and destination warehouses cannot be the same.");

        _logger.LogInformation(
            "Inventory transfer started. Product:{ProductId}, From:{FromWarehouseId}, To:{ToWarehouseId}, Quantity:{Quantity}",
            command.ProductId,
            command.FromWarehouseId,
            command.ToWarehouseId,
            command.Quantity);

        await using var transaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var sourceInventory = await _context.Inventories
                .FirstOrDefaultAsync(
                    x => x.ProductId == command.ProductId &&
                         x.WarehouseId == command.FromWarehouseId,
                    cancellationToken);

            if (sourceInventory is null)
            {
                _logger.LogWarning(
                    "Source inventory not found. Product:{ProductId}, Warehouse:{WarehouseId}",
                    command.ProductId,
                    command.FromWarehouseId);

                throw new NotFoundException("Source inventory not found.");
            }

            var destinationInventory = await _context.Inventories
                .FirstOrDefaultAsync(
                    x => x.ProductId == command.ProductId &&
                         x.WarehouseId == command.ToWarehouseId,
                    cancellationToken);

            if (destinationInventory is null)
            {
                destinationInventory = new InventoryEntity(
                    command.ProductId,
                    command.ToWarehouseId,
                    0);

                _context.Inventories.Add(destinationInventory);

                _logger.LogInformation(
                    "Destination inventory created. Product:{ProductId}, Warehouse:{WarehouseId}",
                    command.ProductId,
                    command.ToWarehouseId);
            }

            sourceInventory.Decrease(command.Quantity);

            destinationInventory.Increase(command.Quantity);

            var referenceNumber = Guid.NewGuid().ToString();

            await _transactionWriter.WriteAsync(
                sourceInventory,
                command.Quantity,
                InventoryTransactionType.TransferOut,
                referenceNumber,
                "Transfer Out",
                cancellationToken);

            await _transactionWriter.WriteAsync(
                destinationInventory,
                command.Quantity,
                InventoryTransactionType.TransferIn,
                referenceNumber,
                "Transfer In",
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Inventory transfer completed successfully. Product:{ProductId}, Quantity:{Quantity}, Reference:{Reference}",
                command.ProductId,
                command.Quantity,
                referenceNumber);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            _logger.LogError(
                ex,
                "Inventory transfer failed. Product:{ProductId}, Quantity:{Quantity}",
                command.ProductId,
                command.Quantity);

            throw;
        }
    }
}