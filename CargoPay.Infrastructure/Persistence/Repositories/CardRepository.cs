using CargoPay.Domain.Entities;
using CargoPay.Infrastructure.Persistence;
using CargoPay.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CargoPay.Infrastructure.Persistence.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Card?> GetByCardNumberAsync(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public async Task AddAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }
    }
}