using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Security;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Auth.Register;

public class RegisterHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Users
            .AnyAsync(x => x.Email == command.Email, cancellationToken);

        if (exists)
            throw new Exception("Email already exists.");

        var user = new User(
            command.FirstName,
            command.LastName,
            command.Email,
            _passwordHasher.Hash(command.Password),
            "Employee");

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}