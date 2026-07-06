
// using MiniERP.Infrastructure.DependencyInjection;
// using MiniERP.API.Endpoints.Categories;
// using MiniERP.API.Middlewares;
// using MiniERP.Application.Features.Products.Create;
// using MiniERP.API.Endpoints.Products;
// using MiniERP.Application;
// using FluentValidation;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using MiniERP.API.Endpoints.Auth;
// var builder = WebApplication.CreateBuilder(args);

// // Add services
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddApplication();
// builder.Services.AddInfrastructure(builder.Configuration);
// // builder.Services.AddScoped<CreateCategoryHandler>();
// // builder.Services.AddScoped<GetAllCategoriesHandler>();
// // builder.Services.AddScoped<GetCategoryByIdHandler>();
// // builder.Services.AddScoped<UpdateCategoryHandler>();
// // builder.Services.AddScoped<DeleteCategoryHandler>();
// // builder.Services.AddScoped<CreateProductHandler>();
// // builder.Services.AddScoped<GetAllProductsHandler>();
// // builder.Services.AddScoped<GetProductByIdHandler>();
// // builder.Services.AddScoped<UpdateProductHandler>();
// // builder.Services.AddScoped<DeleteProductHandler>();

// var provider = builder.Services.BuildServiceProvider();

// var validator = provider.GetService<IValidator<CreateProductCommand>>();

// Console.WriteLine(validator == null ? "Validator NOT Found" : "Validator Found");
// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// app.UseGlobalException();
// app.UseHttpsRedirection();

// app.MapGet("/", () => "Mini ERP API is running.");
// app.MapCategoryEndpoints();
// app.MapGetAllCategoriesEndpoint();
// app.MapGetCategoryByIdEndpoint();
// app.MapUpdateCategoryEndpoint();
// app.MapDeleteCategoryEndpoint();
// app.MapCreateProductEndpoint();
// app.MapGetAllProductsEndpoint();
// app.MapGetProductByIdEndpoint();
// app.MapUpdateProductEndpoint();
// app.MapDeleteProductEndpoint();
// app.MapRegisterEndpoint();
// app.MapLoginEndpoint();
// app.Run();

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

var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalException();

app.UseHttpsRedirection();

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
app.Run();