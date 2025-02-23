using CargoPay.Application.Interfaces.Services;
using CargoPay.Domain.Entities;
using CargoPay.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoPay.Application.Services
{
    public sealed class UFEService : IUFEService
    {
        private decimal _currentFee;
        private DateTime _lastUpdateTime;
        private readonly object _lock = new object();
        private readonly IServiceProvider _serviceProvider;
        public UFEService(IServiceProvider serviceProvider)
        {
            _currentFee = 0.01m;
            //_lastUpdateTime = DateTime.UtcNow;
            _serviceProvider = serviceProvider;
            InitializeCurrentFee().GetAwaiter().GetResult();
        }

        public async Task<decimal> GetCurrentFee()
        {
            FeeHistory? fh = null;
            lock (_lock)
            {
                var now = DateTime.UtcNow;
                if ((now - _lastUpdateTime).TotalHours >= 1)
                {
                    var random = new Random();
                    var multiplier = (decimal)(random.NextDouble() * 2);
                    _currentFee *= multiplier;
                    _lastUpdateTime = now;

                    fh = new FeeHistory()
                    {
                        FeeRate = _currentFee,
                        EffectiveFrom = _lastUpdateTime,
                        EffectiveTo = _lastUpdateTime.AddHours(1)
                    };
                }
            }

            if (fh != null)
            {
                await SaveNewFee(fh);
            }
            return _currentFee;
        }

        private async Task SaveNewFee(FeeHistory fh)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var feeHistoryRepository = scope.ServiceProvider.GetRequiredService<IFeeHistoryRepository>();
                await feeHistoryRepository.AddAsync(fh);
            }
        }
        private async Task InitializeCurrentFee()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var feeHistoryRepository = scope.ServiceProvider.GetRequiredService<IFeeHistoryRepository>();
                var feeHistory = await feeHistoryRepository.GetCurrentFeeHistory();
                if (feeHistory != null)
                {
                    _currentFee = feeHistory.FeeRate;
                    _lastUpdateTime = feeHistory.EffectiveFrom;
                }
            }
        }
    }
}