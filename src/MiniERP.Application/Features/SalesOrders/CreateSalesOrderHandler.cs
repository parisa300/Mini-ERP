using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.SalesOrders.Create;

public class CreateSalesOrderHandler
{
    private readonly IApplicationDbContext _context;

    public CreateSalesOrderHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateSalesOrderCommand command,
        CancellationToken cancellationToken)
    {
        var customerExists = await _context.Customers
            .AnyAsync(x => x.Id == command.CustomerId, cancellationToken);

        if (!customerExists)
            throw new NotFoundException("Customer not found.");

        var warehouseExists = await _context.Warehouses
            .AnyAsync(x => x.Id == command.WarehouseId, cancellationToken);

        if (!warehouseExists)
            throw new NotFoundException("Warehouse not found.");

        foreach (var item in command.Items)
        {
            var productExists = await _context.Products
                .AnyAsync(x => x.Id == item.ProductId, cancellationToken);

            if (!productExists)
                throw new NotFoundException($"Product {item.ProductId} not found.");
        }

        var order = new SalesOrder(
            command.CustomerId,
            command.WarehouseId);

        foreach (var item in command.Items)
        {
            order.AddItem(
                item.ProductId,
                item.Quantity,
                item.UnitPrice);
        }

        _context.SalesOrders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}