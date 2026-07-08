using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Security;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Auth.Register;

public class RegisterHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IApplicationLogger<RegisterHandler> _logger;

    public RegisterHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IApplicationLogger<RegisterHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Registering user with email {Email}",
            command.Email);

        var exists = await _context.Users
            .AnyAsync(
                x => x.Email == command.Email,
                cancellationToken);

        if (exists)
        {
            _logger.LogWarning(
                "Registration failed. Email already exists: {Email}",
                command.Email);

            throw new ConflictException("Email already exists.");
        }

        var user = new User(
            command.FirstName,
            command.LastName,
            command.Email,
            _passwordHasher.Hash(command.Password),
            "Employee");

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User registered successfully. UserId:{UserId}",
            user.Id);

        return user.Id;
    }
}