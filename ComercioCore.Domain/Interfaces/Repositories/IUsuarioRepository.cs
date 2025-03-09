using ComercioCore.Domain.Entities;

namespace ComercioCore.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario user);
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
