using ComercioCore.Domain.Entities;

namespace ComercioCore.Domain.Interfaces.Repositories
{
    public interface IComercianteRepository
    {
        Task<Comerciante> GetByIdAsync(int id);
        IQueryable<Comerciante> GetAll();
        Task AddAsync(Comerciante entity);
        void Update(Comerciante entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<bool> Exists(int id);
    }
}
