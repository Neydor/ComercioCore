using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoPay.Domain.Entities;
using CargoPay.Domain.Interfaces.Repositories;

namespace CargoPay.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment transaction)
        {
            await _context.Payments.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
