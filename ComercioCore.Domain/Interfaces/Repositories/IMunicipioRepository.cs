using ComercioCore.Domain.Entities;

namespace ComercioCore.Domain.Interfaces.Repositories
{
    public interface IMunicipioRepository
    {
        Task<IEnumerable<Municipio>> ObtenerTodos();
    }
}
