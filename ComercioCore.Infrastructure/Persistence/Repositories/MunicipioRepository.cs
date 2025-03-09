using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComercioCore.Infrastructure.Persistence.Repositories
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly ComercioCoreDbContext _context;
        public MunicipioRepository(ComercioCoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Municipio>> ObtenerTodos()
        {
            return await _context.Municipio.ToListAsync();
        }
    }
}
