using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, user.Email, user.Role, token);
    }
}
