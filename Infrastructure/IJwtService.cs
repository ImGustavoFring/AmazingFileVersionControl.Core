
namespace AmazingFileVersionControl.Core.Infrastructure
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string role, string login);
    }
}