using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiniERP.Application;
using MiniERP.Infrastructure.DependencyInjection;
using MiniERP.API.Middlewares;
using MiniERP.API.Endpoints.Categories;
using MiniERP.API.Endpoints.Products;
using MiniERP.API.Endpoints.Auth;
using MiniERP.API.Endpoints.Customers;
using MiniERP.API.Endpoints.Warehouses;
using MiniERP.API.Endpoints.Inventory;
using Serilog;
using MiniERP.API.Endpoints.Suppliers;
using MiniERP.API.Endpoints.PurchaseOrders;
using MiniERP.API.Endpoints.SalesOrders;
using MiniERP.API.Endpoints.Dashboard;
using MiniERP.API.Endpoints.Reports;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddHealthChecks();

// ======================
// Services
// ======================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// ======================
// Middleware
// ======================

app.UseSwagger();
app.UseSwaggerUI();
app.UseGlobalException();

app.UseHttpsRedirection();
app.UseRequestLogging();
app.UseAuthentication();

app.UseAuthorization();

// ======================
// Endpoints
// ======================

app.MapGet("/", () => "Mini ERP API is running.");

// Category
app.MapCategoryEndpoints();
app.MapGetAllCategoriesEndpoint();
app.MapGetCategoryByIdEndpoint();
app.MapUpdateCategoryEndpoint();
app.MapDeleteCategoryEndpoint();

// Product
app.MapCreateProductEndpoint();
app.MapGetAllProductsEndpoint();
app.MapGetProductByIdEndpoint();
app.MapUpdateProductEndpoint();
app.MapDeleteProductEndpoint();

// Auth
app.MapRegisterEndpoint();
app.MapLoginEndpoint();
app.MapRefreshTokenEndpoint();

//Customer
app.MapCreateCustomerEndpoint();
app.MapGetAllCustomersEndpoint();
app.MapGetCustomerByIdEndpoint();
app.MapUpdateCustomerEndpoint();
app.MapDeleteCustomerEndpoint();
//warehouse
app.MapCreateWarehouseEndpoint();
app.MapGetAllWarehousesEndpoint();
app.MapGetWarehouseByIdEndpoint();
app.MapUpdateWarehouseEndpoint();
app.MapDeleteWarehouseEndpoint();
//Inventory
app.MapInitializeInventoryEndpoint();
app.MapReceiveInventoryEndpoint();
app.MapIssueInventoryEndpoint();
app.MapGetInventoryTransactionsEndpoint();
app.MapTransferInventoryEndpoint();
app.MapGetInventoriesEndpoint();

app.MapHealthChecks("/health");

//Supplier
app.MapCreateSupplierEndpoint();
app.MapGetAllSuppliersEndpoint();
app.MapGetSupplierByIdEndpoint();
app.MapUpdateSupplierEndpoint();
app.MapDeleteSupplierEndpoint();

//purchase
app.MapCreatePurchaseOrderEndpoint();
app.MapReceivePurchaseOrderEndpoint();
//sales
app.MapCreateSalesOrderEndpoint();
app.MapConfirmSalesOrderEndpoint();
//Dashboard
app.MapGetDashboardEndpoint();
//Report
app.MapGetSalesReportEndpoint();
app.Run();