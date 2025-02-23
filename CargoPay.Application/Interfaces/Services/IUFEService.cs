using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Application.Interfaces.Services
{
    public interface IUFEService
    {
        Task<decimal> GetCurrentFee();
    }
}
