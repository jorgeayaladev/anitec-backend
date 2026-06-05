using Titan.AniTec.Platform.Iam.Domain.Model;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Resources.Errors;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    private static int ToStatusCodeFromIamError(IamError error)
    {
        return error switch
        {
            IamError.UserNotFound => StatusCodes.Status404NotFound,
            IamError.InvalidCredentials => StatusCodes.Status401Unauthorized,
            IamError.InvalidToken => StatusCodes.Status401Unauthorized,
            IamError.ExpiredToken => StatusCodes.Status401Unauthorized,
            IamError.UserNotActive => StatusCodes.Status403Forbidden,
            IamError.UsernameAlreadyTaken => StatusCodes.Status409Conflict,
            IamError.EmailAlreadyRegistered => StatusCodes.Status409Conflict,
            IamError.InvalidPassword => StatusCodes.Status400BadRequest,
            IamError.OperationCancelled => StatusCodes.Status409Conflict,
            IamError.DatabaseError => StatusCodes.Status500InternalServerError,
            IamError.InternalServerError => StatusCodes.Status500InternalServerError,
            IamError.ExternalServiceError => StatusCodes.Status502BadGateway,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromSignInResult(
        ControllerBase controller,
        Result<(User user, string token)> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<(User user, string token), IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromIamError((IamError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromSignUpResult(
        ControllerBase controller,
        Result<User> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<User, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromIamError((IamError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromCommandResult(
        ControllerBase controller,
        Result result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCodeFromIamError((IamError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetUserByIdResult(
        ControllerBase controller,
        User? user,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<User, IActionResult> successAction)
    {
        if (user is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromIamError(IamError.UserNotFound),
                IamError.UserNotFound,
                errorLocalizer[nameof(IamError.UserNotFound)]
            );
        return successAction(user);
    }
}
