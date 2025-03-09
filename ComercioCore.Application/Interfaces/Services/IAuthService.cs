using ComercioCore.Application.DTOs;
using ComercioCore.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioCore.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(string email, string password);
        //Task RegistrarUser(UsuarioCreateDto userDto);
    }
}
