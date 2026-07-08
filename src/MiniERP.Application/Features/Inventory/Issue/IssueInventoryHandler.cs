using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Inventory.Issue;

public class IssueInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;
    private readonly IApplicationLogger<IssueInventoryHandler> _logger;

    public IssueInventoryHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter,
        IApplicationLogger<IssueInventoryHandler> logger)
    {
        _context = context;
        _transactionWriter = transactionWriter;
        _logger = logger;
    }

    public async Task Handle(
        IssueInventoryCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Inventory issue started. Product:{ProductId}, Warehouse:{WarehouseId}, Quantity:{Quantity}",
            command.ProductId,
            command.WarehouseId,
            command.Quantity);

        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(
                x => x.ProductId == command.ProductId &&
                     x.WarehouseId == command.WarehouseId,
                cancellationToken);

        if (inventory is null)
        {
            _logger.LogWarning(
                "Inventory not found. Product:{ProductId}, Warehouse:{WarehouseId}",
                command.ProductId,
                command.WarehouseId);

            throw new NotFoundException("Inventory not found.");
        }

        inventory.Decrease(command.Quantity);

        var referenceNumber = Guid.NewGuid().ToString();

        await _transactionWriter.WriteAsync(
            inventory,
            command.Quantity,
            InventoryTransactionType.Issue,
            referenceNumber,
            "Issue Inventory",
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Inventory issued successfully. Product:{ProductId}, Quantity:{Quantity}, Reference:{Reference}",
            command.ProductId,
            command.Quantity,
            referenceNumber);
    }
}