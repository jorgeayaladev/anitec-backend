using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}
