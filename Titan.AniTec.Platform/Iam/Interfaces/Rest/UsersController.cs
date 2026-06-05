using System.Net.Mime;
using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Application.QueryServices;
using Titan.AniTec.Platform.Iam.Domain.Model.Commands;
using Titan.AniTec.Platform.Iam.Domain.Model.Queries;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;
using Titan.AniTec.Platform.Resources.Errors;
using Titan.AniTec.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/identity")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(
    IUserQueryService userQueryService,
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet("users/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get user by id",
        Description = "Get a user by its unique identifier",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(query, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromGetUserByIdResult(
            this,
            user,
            errorLocalizer,
            problemDetailsFactory,
            foundUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(foundUser))
        );
    }

    [HttpGet("admin/users")]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users (admin)",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await userQueryService.Handle(query, cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpGet("admin/users/search")]
    [SwaggerOperation(
        Summary = "Search users",
        Description = "Search users by term (admin)",
        OperationId = "SearchUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> SearchUsers([FromQuery] string q, CancellationToken cancellationToken)
    {
        var repo = HttpContext.RequestServices
            .GetRequiredService<Iam.Domain.Repositories.IUserRepository>();
        var users = await repo.SearchAsync(q, cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpPut("admin/users/{userId:int}/status")]
    [SwaggerOperation(
        Summary = "Update user status",
        Description = "Activate or deactivate a user (admin)",
        OperationId = "UpdateUserStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "User status updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> UpdateUserStatus([FromRoute] int userId,
        [FromBody] UpdateUserStatusResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateUserStatusCommand(userId, resource.IsActive);
        var result = await userCommandService.Handle(command, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            () => Ok(new { message = "User status updated" })
        );
    }

    [HttpPut("admin/users/{userId:int}/role")]
    [SwaggerOperation(
        Summary = "Update user role",
        Description = "Update a user's role (admin)",
        OperationId = "UpdateUserRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "User role updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] int userId,
        [FromBody] UpdateUserRoleResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateUserRoleCommand(userId, resource.Role);
        var result = await userCommandService.Handle(command, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            () => Ok(new { message = "User role updated" })
        );
    }
}
