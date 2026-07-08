using System.Security.Cryptography;
using MiniERP.Application.Common.Interfaces.Security;

namespace MiniERP.Infrastructure.Security;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }
}