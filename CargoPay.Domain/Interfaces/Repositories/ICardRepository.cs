using CargoPay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Domain.Interfaces.Repositories
{
    public interface ICardRepository
    {
        Task<Card?> GetByCardNumberAsync(string cardNumber);
        Task AddAsync(Card card);
        Task UpdateAsync(Card card);
    }
}
