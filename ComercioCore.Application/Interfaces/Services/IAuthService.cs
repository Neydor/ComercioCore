using ComercioCore.Application.DTOs.Responses;

namespace ComercioCore.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(string email, string password);
    }
}
