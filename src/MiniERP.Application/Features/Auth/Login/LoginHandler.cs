using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Interfaces.Security;
using MiniERP.Application.Common.Security;
using RefreshTokenEntity = MiniERP.Domain.Entities.RefreshToken;

namespace MiniERP.Application.Features.Auth.Login;

public class LoginHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IApplicationLogger<LoginHandler> _logger;

    public LoginHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IApplicationLogger<LoginHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Login attempt for {Email}",
            command.Email);

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == command.Email && x.IsActive,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var validPassword = _passwordHasher.Verify(
            command.Password,
            user.PasswordHash);

        if (!validPassword)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var oldTokens = await _context.RefreshTokens
            .Where(x =>
                x.UserId == user.Id &&
                !x.IsRevoked)
            .ToListAsync(cancellationToken);

        foreach (var token in oldTokens)
        {
            token.Revoke();
        }

        var accessToken = _jwtTokenGenerator.Generate(user);

        var refreshToken = _refreshTokenGenerator.Generate();

        _context.RefreshTokens.Add(
            new RefreshTokenEntity(
                user.Id,
                refreshToken,
                DateTime.UtcNow.AddDays(7)));

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User {UserId} logged in successfully.",
            user.Id);

        return new LoginResponse(
            accessToken,
            refreshToken);
    }
}