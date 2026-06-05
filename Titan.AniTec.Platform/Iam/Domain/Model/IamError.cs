namespace Titan.AniTec.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    UsernameAlreadyTaken,
    EmailAlreadyRegistered,
    InvalidCredentials,
    InvalidToken,
    ExpiredToken,
    UserNotActive,
    EmailNotVerified,
    InvalidPassword,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}
