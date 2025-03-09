using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComercioCore.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ComercioCoreDbContext _context;
        public UsuarioRepository(ComercioCoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario user)
        {
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == email);
        }
    }
}
