using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.IsActive,
            user.EmailVerified,
            user.CreatedAt
        );
    }
}
