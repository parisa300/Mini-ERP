using MiniERP.Domain.Entities;

namespace MiniERP.Application.Common.Security;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}