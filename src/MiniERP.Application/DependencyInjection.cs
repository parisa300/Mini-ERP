using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniERP.Application.Features.Auth.Login;
using MiniERP.Application.Features.Auth.RefreshToken;
using MiniERP.Application.Features.Auth.Register;
using MiniERP.Application.Features.Categories.Create;
using MiniERP.Application.Features.Categories.Delete;
using MiniERP.Application.Features.Categories.GetAll;
using MiniERP.Application.Features.Categories.GetById;
using MiniERP.Application.Features.Categories.Update;
using MiniERP.Application.Features.Customers.Create;
using MiniERP.Application.Features.Customers.Delete;
using MiniERP.Application.Features.Customers.GetAll;
using MiniERP.Application.Features.Customers.GetById;
using MiniERP.Application.Features.Customers.Update;
using MiniERP.Application.Features.Inventory.GetInventories;
using MiniERP.Application.Features.Inventory.GetTransactions;
using MiniERP.Application.Features.Inventory.Initialize;
using MiniERP.Application.Features.Inventory.Issue;
using MiniERP.Application.Features.Inventory.Receive;
using MiniERP.Application.Features.Inventory.Transfer;
using MiniERP.Application.Features.Products.Create;
using MiniERP.Application.Features.Products.Delete;
using MiniERP.Application.Features.Products.GetAll;
using MiniERP.Application.Features.Products.GetById;
using MiniERP.Application.Features.Products.Update;
using MiniERP.Application.Features.PurchaseOrders.Create;
using MiniERP.Application.Features.Suppliers.Create;
using MiniERP.Application.Features.Suppliers.Delete;
using MiniERP.Application.Features.Suppliers.GetAll;
using MiniERP.Application.Features.Suppliers.GetById;
using MiniERP.Application.Features.Suppliers.Update;
using MiniERP.Application.Features.Warehouses.Create;
using MiniERP.Application.Features.Warehouses.Delete;
using MiniERP.Application.Features.Warehouses.GetAll;
using MiniERP.Application.Features.Warehouses.GetById;
using MiniERP.Application.Features.Warehouses.Update;


namespace MiniERP.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Category
        services.AddScoped<CreateCategoryHandler>();
        services.AddScoped<GetAllCategoriesHandler>();
        services.AddScoped<GetCategoryByIdHandler>();
        services.AddScoped<UpdateCategoryHandler>();
        services.AddScoped<DeleteCategoryHandler>();

        // Product
        services.AddScoped<CreateProductHandler>();
        services.AddScoped<GetAllProductsHandler>();
        services.AddScoped<GetProductByIdHandler>();
        services.AddScoped<UpdateProductHandler>();
        services.AddScoped<DeleteProductHandler>();

        //User
        services.AddScoped<RegisterHandler>();
        services.AddScoped<LoginHandler>();
        services.AddScoped<RefreshTokenHandler>();
        //Customer
        services.AddScoped<CreateCustomerHandler>();
        services.AddScoped<GetAllCustomersHandler>();
        services.AddScoped<GetCustomerByIdHandler>();
        services.AddScoped<UpdateCustomerHandler>();
        services.AddScoped<DeleteCustomerHandler>();

        //warehouse
        services.AddScoped<CreateWarehouseHandler>();
        services.AddScoped<GetAllWarehousesHandler>();
        services.AddScoped<GetWarehouseByIdHandler>();
        services.AddScoped<UpdateWarehouseHandler>();
        services.AddScoped<DeleteWarehouseHandler>();

        //Inventory
        services.AddScoped<InitializeInventoryHandler>();
        services.AddScoped<ReceiveInventoryHandler>();
        services.AddScoped<IssueInventoryHandler>();
        services.AddScoped<GetInventoryTransactionsHandler>();
        services.AddScoped<TransferInventoryHandler>();
        services.AddScoped<GetInventoriesHandler>();

        //Supplier
        services.AddScoped<CreateSupplierHandler>();
        services.AddScoped<GetAllSuppliersHandler>();
        services.AddScoped<GetSupplierByIdHandler>();
        services.AddScoped<UpdateSupplierHandler>();
        services.AddScoped<DeleteSupplierHandler>();

        //purchase
        services.AddScoped<CreatePurchaseOrderHandler>();
        return services;
    }
}