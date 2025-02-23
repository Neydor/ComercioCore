using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Domain.Entities
{
    public class FeeHistory
    {
        public int Id { get; set; }
        public decimal FeeRate { get; set; } // e.g., 0.015 for 1.5%
        public DateTime EffectiveFrom { get; set; } // When this fee became active
        public DateTime? EffectiveTo { get; set; } // When it was superseded (null if current)
    }
}
