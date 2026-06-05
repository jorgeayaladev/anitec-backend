using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Application.QueryServices;
using Titan.AniTec.Platform.Iam.Domain.Model.Commands;
using Titan.AniTec.Platform.Iam.Domain.Model.Queries;
using Titan.AniTec.Platform.Iam.Interfaces.Acl;

namespace Titan.AniTec.Platform.Iam.Application.Acl;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string email, string password, string role,
        CancellationToken cancellationToken)
    {
        var signUpCommand = new SignUpCommand(username, email, password, role);
        var signUpResult = await userCommandService.Handle(signUpCommand, cancellationToken);
        if (signUpResult.IsFailure) return 0;
        return signUpResult.Value?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByUsername(string username, CancellationToken cancellationToken)
    {
        var query = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(query, cancellationToken);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(query, cancellationToken);
        return result?.Username ?? string.Empty;
    }

    public async Task<bool> UserExistsByEmail(string email, CancellationToken cancellationToken)
    {
        var query = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(query, cancellationToken);
        return result != null;
    }
}
