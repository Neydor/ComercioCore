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
        public decimal FeeRate { get; set; }
        public DateTime EffectiveFrom { get; set; } 
        public DateTime? EffectiveTo { get; set; } 
    }
}
