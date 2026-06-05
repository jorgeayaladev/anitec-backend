using System.Net.Mime;
using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;
using Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;
using Titan.AniTec.Platform.Iam.Resources;
using Titan.AniTec.Platform.Resources.Errors;
using Titan.AniTec.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/identity")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource,
        CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            userAndToken =>
                Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(userAndToken.user,
                    userAndToken.token))
        );
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Sign up a new user",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created successfully", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user was not created")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource,
        CancellationToken cancellationToken)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            createdUser => CreatedAtAction(nameof(UsersController.GetUserById), "Users",
                new { userId = createdUser.Id },
                UserResourceFromEntityAssembler.ToResourceFromEntity(createdUser))
        );
    }

    [HttpPost("change-password")]
    [SwaggerOperation(
        Summary = "Change password",
        Description = "Change password for authenticated user",
        OperationId = "ChangePassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid current password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResource resource,
        CancellationToken cancellationToken)
    {
        var user = HttpContext.Items["User"] as Iam.Domain.Model.Aggregates.User;
        if (user == null) return Unauthorized();

        var command = new Iam.Domain.Model.Commands.ChangePasswordCommand(user.Id, resource.CurrentPassword,
            resource.NewPassword);
        var result = await userCommandService.Handle(command, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            () => Ok(new { message = iamLocalizer["PasswordChangedSuccessfully"] })
        );
    }
}
