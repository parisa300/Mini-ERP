using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Interfaces.Security;
using MiniERP.Application.Common.Security;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Auth.RefreshToken;

public class RefreshTokenHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public RefreshTokenHandler(
        IApplicationDbContext context,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(
                x => x.Token == command.RefreshToken,
                cancellationToken);

        if (refreshToken is null)
            throw new Exception("Invalid refresh token.");

        if (refreshToken.IsExpired)
            throw new Exception("Refresh token expired.");

        if (refreshToken.IsRevoked)
            throw new Exception("Refresh token revoked.");

        refreshToken.Revoke();

        var newRefreshToken =
            _refreshTokenGenerator.Generate();

        _context.RefreshTokens.Add(
            new MiniERP.Domain.Entities.RefreshToken(
                refreshToken.UserId,
                newRefreshToken,
                DateTime.UtcNow.AddDays(7)));

        await _context.SaveChangesAsync(cancellationToken);

        var accessToken =
            _jwtTokenGenerator.Generate(refreshToken.User);

        return new RefreshTokenResponse(
            accessToken,
            newRefreshToken);
    }
}