using ComercioCore.Domain.Entities;

namespace ComercioCore.Domain.Interfaces.Repositories
{
    public interface IEstablecimientoRepository
    {
        Task AddAsync(Establecimiento establecimiento);
    }
}
