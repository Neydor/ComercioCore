using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComercioCore.Infrastructure.Persistence.Repositories
{
    public class EstablecimientoRepository : IEstablecimientoRepository
    {
        ComercioCoreDbContext _context;

        public EstablecimientoRepository(ComercioCoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Establecimiento establecimiento)
        {
            await _context.Establecimientos.AddAsync(establecimiento);
            await _context.SaveChangesAsync();
        }

    }
}
