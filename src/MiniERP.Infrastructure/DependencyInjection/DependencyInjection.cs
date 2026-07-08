using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniERP.Infrastructure.Persistence;
using MiniERP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using MiniERP.Application.Common.Security;
using MiniERP.Infrastructure.Security;
using MiniERP.Infrastructure.Services;
using MiniERP.Application.Features.Inventory.Transfer;
using MiniERP.Infrastructure.Logging;
using MiniERP.Application.Common.Interfaces.Security;
namespace MiniERP.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
     services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")));

     services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddScoped<IInventoryTransactionWriter, InventoryTransactionWriter>();
 //   services.AddScoped<IInventoryTransferService, InventoryTransferService>();
   services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
   services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        return services;
    }
    
}