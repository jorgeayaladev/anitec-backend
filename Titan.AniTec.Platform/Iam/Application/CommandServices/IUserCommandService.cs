using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Domain.Model.Commands;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);
    Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken);
}
