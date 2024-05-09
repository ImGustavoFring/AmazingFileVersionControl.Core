﻿
namespace AmazingFileVersionControl.Core.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string loginOrEmail, string password);
        Task<string> RegisterAsync(string login, string email, string password);
    }
}