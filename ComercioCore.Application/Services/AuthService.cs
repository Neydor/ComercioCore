using ComercioCore.Application.Common;
using ComercioCore.Application.DTOs;
using ComercioCore.Application.DTOs.Responses;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IOptions<JwtSettings> jwtSettings,
        IUsuarioRepository usuarioRepository,
        ILogger<AuthService> logger
        )
    {
        _jwtSettings = jwtSettings.Value;
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<AuthResponse> Login(string email, string password)
    {
        try
        {
            var user = await _usuarioRepository.GetByEmailAsync(email);

            if (user == null || !user.Contrasena.Equals(password))
            {
                _logger.LogWarning("Intento de inicio de sesión fallido para el email: {Email}", email);
                return new AuthResponse { Success = false, Message = "Credenciales inválidas" };
            }

            var (token, expiration) = GenerateJwtToken(user);
            _logger.LogInformation("Inicio de sesión exitoso para el usuario: {UserId}", user.Id);
            return new AuthResponse
            {
                Success = true,
                Token = token,
                Expiration = expiration
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante el proceso de autenticación para el email: {Email}", email);
            return new AuthResponse { Success = false, Message = "Error durante el proceso de autenticación, intentelo de nuevo." };
        }

    }

    private (string token, DateTime expiration) GenerateJwtToken(Usuario user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var expirationTime = DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.CorreoElectronico),
                new Claim(ClaimTypes.Role, user.Rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = expirationTime,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return(tokenHandler.WriteToken(token), expirationTime);
    }
}