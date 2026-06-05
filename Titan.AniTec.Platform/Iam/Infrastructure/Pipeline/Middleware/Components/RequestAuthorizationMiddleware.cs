using Titan.AniTec.Platform.Iam.Application.Internal.OutboundServices;
using Titan.AniTec.Platform.Iam.Application.QueryServices;
using Titan.AniTec.Platform.Iam.Domain.Model.Queries;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);

        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);

        context.Items["User"] = user;
        await next(context);
    }
}
