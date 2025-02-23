using CargoPay.Domain.Entities;
using CargoPay.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CargoPay.Infrastructure.Persistence.Repositories
{
    public class FeeHistoryRepository : IFeeHistoryRepository
    {
        ApplicationDbContext _context;

        public FeeHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FeeHistory feeHistory)
        {
            await _context.FeesHistory.AddAsync(feeHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<FeeHistory?> GetCurrentFeeHistory()
        {
            DateTime date = DateTime.UtcNow;
            return await _context.FeesHistory.FirstOrDefaultAsync(x => x.EffectiveFrom < date && x.EffectiveTo > date);
        }
    }
}
