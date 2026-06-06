using Titan.AniTec.Platform.Iam.Domain.Model;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;

public static class RoleAssembler
{
    public static RoleResource ToResource(Role entity)
        => new(entity.Id, entity.Name, entity.Description);

    public static Role ToEntity(CreateRoleResource resource)
        => new(resource.Name, resource.Description);
}

public static class RoleActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            IamError.RoleNotFound => new NotFoundResult(),
            IamError.RoleAlreadyExists => new ConflictResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error switch
        {
            IamError.RoleNotFound => new NotFoundResult(),
            IamError.RoleAlreadyExists => new ConflictResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new CreatedResult(string.Empty, result.Value);

        return ToActionResult(result);
    }
}
