namespace Titan.AniTec.Platform.Iam.Domain.Model.Commands;

public record SignUpCommand(string Username, string Email, string Password, string Role);
