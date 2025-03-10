using ComercioCore.Application.DTOs.Resquests;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain;
using ComercioCore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComercioCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _authService.Login(request.CorreoElectronico, request.Contrasena);

            return result.Success ?
                Ok(result) :
                Unauthorized(result);
        }
    }
}
