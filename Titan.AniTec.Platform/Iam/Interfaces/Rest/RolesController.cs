using System.Net.Mime;
using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Domain.Model;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Domain.Repositories;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/identity")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Role endpoints")]
public class RolesController(
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IUserCommandService userCommandService,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("roles")]
    [SwaggerOperation(Summary = "List all roles", OperationId = "GetAllRoles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roles retrieved", typeof(IEnumerable<RoleResource>))]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await roleRepository.ListAllAsync(cancellationToken);
        return Ok(roles.Select(RoleAssembler.ToResource));
    }

    [HttpGet("roles/{roleId:int}")]
    [SwaggerOperation(Summary = "Get role by id", OperationId = "GetRoleById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role found", typeof(RoleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> GetRoleById(int roleId, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FindByIdAsync(roleId, cancellationToken);
        if (role == null) return NotFound();
        return Ok(RoleAssembler.ToResource(role));
    }

    [HttpPost("roles")]
    [SwaggerOperation(Summary = "Create role", OperationId = "CreateRole")]
    [SwaggerResponse(StatusCodes.Status201Created, "Role created", typeof(RoleResource))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Role already exists")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleResource resource,
        CancellationToken cancellationToken)
    {
        var existing = await roleRepository.FindByNameAsync(resource.Name, cancellationToken);
        if (existing != null)
            return Conflict(new { message = "Role already exists" });

        var role = RoleAssembler.ToEntity(resource);
        await roleRepository.AddAsync(role, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return CreatedAtAction(nameof(GetRoleById), new { roleId = role.Id }, RoleAssembler.ToResource(role));
    }

    [HttpPut("roles/{roleId:int}")]
    [SwaggerOperation(Summary = "Update role", OperationId = "UpdateRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role updated", typeof(RoleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] UpdateRoleResource resource,
        CancellationToken cancellationToken)
    {
        var role = await roleRepository.FindByIdAsync(roleId, cancellationToken);
        if (role == null) return NotFound();

        role.UpdateDetails(resource.Name, resource.Description);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(RoleAssembler.ToResource(role));
    }

    [HttpDelete("roles/{roleId:int}")]
    [SwaggerOperation(Summary = "Delete role", OperationId = "DeleteRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> DeleteRole(int roleId, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FindByIdAsync(roleId, cancellationToken);
        if (role == null) return NotFound();

        roleRepository.Remove(role);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(new { message = "Role deleted" });
    }

    [HttpPost("users/{userId:int}/roles")]
    [SwaggerOperation(Summary = "Assign role to user", OperationId = "AssignUserRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role assigned")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> AssignUserRole(int userId, [FromBody] UpdateUserRoleResource resource,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(userId, cancellationToken);
        if (user == null) return NotFound();

        user.UpdateRole(resource.Role);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(new { message = "Role assigned" });
    }

    [HttpDelete("users/{userId:int}/roles/{roleId:int}")]
    [SwaggerOperation(Summary = "Remove role from user", OperationId = "RemoveUserRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role removed")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> RemoveUserRole(int userId, int roleId,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(userId, cancellationToken);
        if (user == null) return NotFound();

        user.UpdateRole("farmer");
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(new { message = "Role removed" });
    }
}
