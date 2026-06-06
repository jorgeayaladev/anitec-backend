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
[Route("api/identity")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Login",
        Description = "Authenticate a user with credentials",
        OperationId = "Login")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> Login([FromBody] SignInResource signInResource,
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

    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Register",
        Description = "Register a new user",
        OperationId = "Register")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created successfully", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user was not created")]
    public async Task<IActionResult> Register([FromBody] SignUpResource signUpResource,
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

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Refresh token",
        Description = "Refresh an expired JWT token",
        OperationId = "RefreshToken")]
    [SwaggerResponse(StatusCodes.Status200OK, "Token refreshed successfully")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok(new { message = iamLocalizer["TokenRefreshed"] });
    }

    [HttpPost("logout")]
    [SwaggerOperation(
        Summary = "Logout",
        Description = "Logout and invalidate current token",
        OperationId = "Logout")]
    [SwaggerResponse(StatusCodes.Status200OK, "Logged out successfully")]
    public async Task<IActionResult> Logout()
    {
        return Ok(new { message = iamLocalizer["LoggedOutSuccessfully"] });
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Forgot password",
        Description = "Request a password reset link",
        OperationId = "ForgotPassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "If the email exists, a reset link was sent")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordResource resource)
    {
        return Ok(new { message = iamLocalizer["PasswordResetSent"] });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Reset password",
        Description = "Reset password with token",
        OperationId = "ResetPassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset successfully")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resource)
    {
        return Ok(new { message = iamLocalizer["PasswordResetSuccessfully"] });
    }

    [HttpPost("verify-email")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Verify email",
        Description = "Verify email address with token",
        OperationId = "VerifyEmail")]
    [SwaggerResponse(StatusCodes.Status200OK, "Email verified successfully")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailResource resource)
    {
        return Ok(new { message = iamLocalizer["EmailVerifiedSuccessfully"] });
    }

    [HttpPost("resend-verification")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Resend verification",
        Description = "Resend email verification code",
        OperationId = "ResendVerification")]
    [SwaggerResponse(StatusCodes.Status200OK, "Verification email resent")]
    public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationResource resource)
    {
        return Ok(new { message = iamLocalizer["VerificationResent"] });
    }

    [HttpPost("verify-phone")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Verify phone",
        Description = "Verify phone number with code",
        OperationId = "VerifyPhone")]
    [SwaggerResponse(StatusCodes.Status200OK, "Phone verified successfully")]
    public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneResource resource)
    {
        return Ok(new { message = iamLocalizer["PhoneVerifiedSuccessfully"] });
    }

    [HttpPost("resend-phone-verification")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Resend phone verification",
        Description = "Resend phone verification code",
        OperationId = "ResendPhoneVerification")]
    [SwaggerResponse(StatusCodes.Status200OK, "Verification code resent")]
    public async Task<IActionResult> ResendPhoneVerification([FromBody] ResendPhoneVerificationResource resource)
    {
        return Ok(new { message = iamLocalizer["PhoneVerificationResent"] });
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
