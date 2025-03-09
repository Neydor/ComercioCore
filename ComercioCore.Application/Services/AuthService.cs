using ComercioCore.Application.Common;
using ComercioCore.Application.DTOs;
using ComercioCore.Application.DTOs.Responses;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IOptions<JwtSettings> jwtSettings,
        IUsuarioRepository usuarioRepository
        )
    {
        _jwtSettings = jwtSettings.Value;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<AuthResponse> Login(string email, string password)
    {
        var user = await _usuarioRepository.GetByEmailAsync(email);

        if (user == null || user.Contrasena.Equals(password))
        {
            return new AuthResponse { Success = false, Message = "Credenciales inválidas" };
        }

        var token = GenerateJwtToken(user);
        return new AuthResponse { Success = true, Token = token };
    }
    
    private string GenerateJwtToken(Usuario user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.CorreoElectronico),
                new Claim(ClaimTypes.Role, user.Rol)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}