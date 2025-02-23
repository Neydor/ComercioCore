using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Application.DTOs.Resquests
{
    public class PayRequest
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
