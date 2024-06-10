namespace BlogApp.WebAPI.Contracts;

public interface IAuthService
{
    bool Authorize(string username, string password);

    string GenerateToken(string username);

    bool ValidateToken(string token);
}
