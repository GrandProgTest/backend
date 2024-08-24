using irs.API.IAM.Domain.Model.Aggregates;

namespace irs.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}