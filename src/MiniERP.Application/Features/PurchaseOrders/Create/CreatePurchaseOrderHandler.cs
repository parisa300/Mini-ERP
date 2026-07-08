using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.PurchaseOrders.Create;

public class CreatePurchaseOrderHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationLogger<CreatePurchaseOrderHandler> _logger;

    public CreatePurchaseOrderHandler(
        IApplicationDbContext context,
        IApplicationLogger<CreatePurchaseOrderHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        CreatePurchaseOrderCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating purchase order. Supplier:{SupplierId}, Warehouse:{WarehouseId}, Items:{ItemsCount}",
            command.SupplierId,
            command.WarehouseId,
            command.Items.Count);

        if (command.Items.Count == 0)
        {
            _logger.LogWarning(
                "Purchase order has no items. Supplier:{SupplierId}",
                command.SupplierId);

            throw new ValidationException(
                "Purchase order must contain at least one item.");
        }

        var supplierExists = await _context.Suppliers
            .AnyAsync(
                x => x.Id == command.SupplierId,
                cancellationToken);

        if (!supplierExists)
        {
            _logger.LogWarning(
                "Supplier not found. Supplier:{SupplierId}",
                command.SupplierId);

            throw new NotFoundException("Supplier not found.");
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

        var order = new PurchaseOrder(
            command.SupplierId,
            command.WarehouseId);

        foreach (var item in command.Items)
        {
            order.AddItem(
                item.ProductId,
                item.Quantity,
                item.UnitPrice);
        }

        order.Submit();

        _context.PurchaseOrders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Purchase order created successfully. Order:{OrderId}",
            order.Id);

        return order.Id;
    }
}