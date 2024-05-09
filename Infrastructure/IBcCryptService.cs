namespace AmazingFileVersionControl.Core.Infrastructure
{
    public interface IBcCryptService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}