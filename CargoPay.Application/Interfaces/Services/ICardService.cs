using CargoPay.Domain.Entities;

namespace CargoPay.Application.Interfaces.Services
{
    public interface ICardService
    {
        Task<string> CreateCardAsync(string userId, decimal balance);
        Task<Guid> PayAsync(string cardNumber, decimal amount);
        Task<decimal> GetBalanceAsync(string cardNumber);
        Task<Card> GetCard(string cardNumber);
    }
}
