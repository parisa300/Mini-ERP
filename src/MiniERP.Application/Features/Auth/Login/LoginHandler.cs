using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Security;

namespace MiniERP.Application.Features.Auth.Login;

public class LoginHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == command.Email &&
                x.IsActive,
                cancellationToken);

        if (user is null)
            throw new Exception("Invalid email or password.");

        var validPassword = _passwordHasher.Verify(
            command.Password,
            user.PasswordHash);

        if (!validPassword)
            throw new Exception("Invalid email or password.");

        return _jwtTokenGenerator.Generate(user);
    }
}