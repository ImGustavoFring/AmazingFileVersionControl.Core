
namespace AmazingFileVersionControl.Core.Infrastructure
{
    public interface IJwtGenerator
    {
        string GenerateToken(Guid userId, string role, string login);
    }
}