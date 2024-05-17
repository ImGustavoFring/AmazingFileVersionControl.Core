
namespace AmazingFileVersionControl.Core.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string login, string password);
        Task<string> RegisterAsync(string login, string email, string password);
    }
}