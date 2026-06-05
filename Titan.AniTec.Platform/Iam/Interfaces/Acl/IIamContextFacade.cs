namespace Titan.AniTec.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> CreateUser(string username, string email, string password, string role, CancellationToken cancellationToken);
    Task<int> FetchUserIdByUsername(string username, CancellationToken cancellationToken);
    Task<string> FetchUsernameByUserId(int userId, CancellationToken cancellationToken);
    Task<bool> UserExistsByEmail(string email, CancellationToken cancellationToken);
}
