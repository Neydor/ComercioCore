using CargoPay.Application.Interfaces.Services;
using CargoPay.Domain.Entities;
using CargoPay.Domain.Interfaces.Repositories;
using CargoPay.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Memory;

namespace CargoPay.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUFEService _ufeService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;
        public CardService(ApplicationDbContext dbContext,
            ICardRepository cardRepository, 
            IPaymentRepository paymentRepository,
            IMemoryCache cache,
            IUFEService ufeService)
        {
            _dbContext = dbContext;
            _cardRepository = cardRepository;
            _paymentRepository = paymentRepository;
            _cache = cache;
            _ufeService = ufeService;
        }

        public async Task<string> CreateCardAsync(string userId, decimal balance)
        {
            string cardNumber;
            do
            {
                cardNumber = GenerateRandomCardNumber();
            } while (await _cardRepository.GetByCardNumberAsync(cardNumber) != null);

            var card = new Card
            {
                CardNumber = cardNumber,
                Balance = balance, 
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _cardRepository.AddAsync(card);
            return cardNumber;
        }

        public async Task<Guid> PayAsync(string cardNumber, decimal amount)
        {
            // Start a transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Fetch the card
                var card = await _cardRepository.GetByCardNumberAsync(cardNumber);
                if (card == null) throw new Exception("Card not found");

                // Calculate fee and total deduction
                var feeRate = await _ufeService.GetCurrentFee();
                var fee = amount * feeRate;
                var totalDeduction = amount + fee;

                // Check balance
                if (card.Balance < totalDeduction) throw new Exception("Insufficient balance");

                // Update card balance
                card.Balance -= totalDeduction;
                await _cardRepository.UpdateAsync(card);

                // Record the transaction
                var paymentTransaction = new Payment
                {
                    CardId = card.CardId, 
                    Amount = amount,
                    Fee = fee,
                    Timestamp = DateTime.UtcNow
                };
                await _paymentRepository.AddAsync(paymentTransaction);

                // Commit the transaction
                await transaction.CommitAsync();
                //Remove in cache, is updated the balance
                _cache.Remove(cardNumber);
                return paymentTransaction.PaymentId;
            }
            catch (Exception)
            {
                // Rollback on error
                await transaction.RollbackAsync();
                throw; // Re-throw the exception for the controller to handle
            }
        }

        public async Task<decimal> GetBalanceAsync(string cardNumber)
        {
            if (!_cache.TryGetValue(cardNumber, out Card? cardInCache))
            {
                var card = await _cardRepository.GetByCardNumberAsync(cardNumber);
                if (card == null) throw new Exception("Card not found");
                cardInCache = card;
                _cache.Set(cardNumber, cardInCache, TimeSpan.FromMinutes(5));
            }
            return cardInCache.Balance;
        }
        public async Task<Card> GetCard(string cardNumber)
        {
            if (!_cache.TryGetValue(cardNumber, out Card? cardInCache))
            {
                var card = await _cardRepository.GetByCardNumberAsync(cardNumber);
                if (card == null) throw new Exception("Card not found");
                cardInCache = card;
                _cache.Set(cardNumber, cardInCache, TimeSpan.FromMinutes(5));
            }
            return cardInCache;
        }

        private string GenerateRandomCardNumber()
        {
            var random = new Random();
            long number = (long)(random.NextDouble() * 900000000000000) + 100000000000000;
            return number.ToString();
        }
    }
}
