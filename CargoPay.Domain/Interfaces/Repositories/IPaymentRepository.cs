using CargoPay.Domain.Entities;

namespace CargoPay.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
    }
}
