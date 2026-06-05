using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Application.Internal.OutboundServices;
using Titan.AniTec.Platform.Iam.Domain.Model;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Domain.Model.Commands;
using Titan.AniTec.Platform.Iam.Domain.Repositories;
using Titan.AniTec.Platform.Resources.Errors;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Titan.AniTec.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IUserCommandService
{
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamError.InvalidCredentials,
                localizer[nameof(IamError.InvalidCredentials)]);

        if (!user.IsActive)
            return Result<(User user, string token)>.Failure(IamError.UserNotActive,
                localizer[nameof(IamError.UserNotActive)]);

        user.MarkLogin();
        userRepository.Update(user);
        await unitOfWork.CompleteAsync(cancellationToken);

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
            return Result<User>.Failure(IamError.UsernameAlreadyTaken,
                localizer[nameof(IamError.UsernameAlreadyTaken), command.Username]);

        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result<User>.Failure(IamError.EmailAlreadyRegistered,
                localizer[nameof(IamError.EmailAlreadyRegistered), command.Email]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, command.Email, hashedPassword, command.Role);

        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(IamError.OperationCancelled, localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<User>.Failure(IamError.InternalServerError, localizer[nameof(IamError.InternalServerError)]);
        }
    }

    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            return Result.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);

        if (!hashingService.VerifyPassword(command.CurrentPassword, user.PasswordHash))
            return Result.Failure(IamError.InvalidPassword, localizer[nameof(IamError.InvalidPassword)]);

        var newHashedPassword = hashingService.HashPassword(command.NewPassword);
        user.UpdatePasswordHash(newHashedPassword);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
        }
    }

    public async Task<Result> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            return Result.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);

        if (command.IsActive)
            user.Activate();
        else
            user.Deactivate();

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
        }
    }

    public async Task<Result> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            return Result.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);

        user.UpdateRole(command.Role);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
        }
    }
}
