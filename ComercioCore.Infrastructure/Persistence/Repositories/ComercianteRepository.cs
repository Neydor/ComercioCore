using ComercioCore.Domain.Entities;
using ComercioCore.Infrastructure.Persistence;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComercioCore.Infrastructure.Persistence.Repositories
{
    public class ComercianteRepository : IComercianteRepository
    {
        private readonly ComercioCoreDbContext _context;

        public ComercianteRepository(ComercioCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Comerciante> GetByIdAsync(int id)
        {
            return await _context.Comerciantes
                .Include(c => c.Municipio)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Comerciante> GetAll()
        {
            return _context.Comerciantes
                .Include(c => c.Municipio)
                .AsQueryable();
        }

        public async Task AddAsync(Comerciante entity)
        {
            await _context.Comerciantes.AddAsync(entity);
        }

        public void Update(Comerciante entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Comerciantes.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Comerciantes.AnyAsync(c => c.Id == id);
        }
    }
}