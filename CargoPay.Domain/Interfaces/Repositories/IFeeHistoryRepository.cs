using CargoPay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Domain.Interfaces.Repositories
{
    public interface IFeeHistoryRepository
    {
        Task<FeeHistory?> GetCurrentFeeHistory();
        Task AddAsync(FeeHistory feeHistory);
    }
}
