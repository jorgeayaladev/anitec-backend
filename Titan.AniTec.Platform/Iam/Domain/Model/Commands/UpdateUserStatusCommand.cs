namespace Titan.AniTec.Platform.Iam.Domain.Model.Commands;

public record UpdateUserStatusCommand(int UserId, bool IsActive);
